using System.Diagnostics;
using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Utils; 
using QRCoder;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace DesafioFundamentos.Services;

public class PdfService
{
    public static void GerarComprovantePdf(Ticket ticket)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        // ⚙️ CONFIGURE OS SEUS DADOS DO PIX AQUI:
        string chavePix = "name@email.com"; // Seu CPF, CNPJ, E-mail, Telefone ou Chave Aleatória
        string nomeTitular = "Name"; // Nome do titular da conta Pix
        string cidadeTitular = "City"; // Cidade do titular da conta Pix

        // 1. Gera o Payload oficial do Pix com o valor exato do Ticket
        string payloadPix = PixHelper.GerarPayloadStatic(
            chavePix,
            nomeTitular,
            cidadeTitular,
            ticket.ValorPago,
            $"TICKET{ticket.Numero}");

        // 2. Converte a string do Pix para imagem do QR Code
        byte[] qrCodeBytes = GerarBytesQrCode(payloadPix);

        // 3. Pasta onde o PDF será salvo
        string pastaPdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comprovantes");
        Directory.CreateDirectory(pastaPdf);

        string caminhoArquivo = Path.Combine(pastaPdf, $"Ticket_{ticket.Numero}_{ticket.Placa}.pdf");

        // 4. Montagem do Cupom Fiscal em PDF
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A6);
                page.Margin(15);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Header()
                    .AlignCenter()
                    .Text("COMPROVANTE DE ESTACIONAMENTO")
                    .Bold()
                    .FontSize(11);

                page.Content().PaddingVertical(10).Column(col =>
                {
                    col.Item().LineHorizontal(1);
                    col.Item().PaddingVertical(5).Text($"Ticket Nº    : {ticket.Numero}").Bold();
                    col.Item().Text($"Placa        : {ticket.Placa}");
                    col.Item().Text($"Entrada      : {ticket.Entrada:dd/MM/yyyy HH:mm:ss}");
                    col.Item().Text($"Saída        : {ticket.Saida:dd/MM/yyyy HH:mm:ss}");
                    col.Item().Text($"Permanência  : {ticket.Permanencia:hh\\:mm\\:ss}");
                    col.Item().PaddingTop(5).Text($"Valor a Pagar: R$ {ticket.ValorPago:F2}").Bold().FontSize(12);
                    col.Item().LineHorizontal(1);

                    // Adiciona o QR Code do Pix no centro do PDF
                    col.Item().PaddingTop(10).AlignCenter().Width(110).Image(qrCodeBytes);
                    col.Item().AlignCenter().Text("Pague via Pix escaneando com o app do seu banco").FontSize(8).Italic();
                });

                page.Footer()
                    .AlignCenter()
                    .Text("Obrigado pela preferência!")
                    .FontSize(8);
            });
        })
        .GeneratePdf(caminhoArquivo);

        Console.WriteLine($"\n📄 Comprovante PDF gerado em: {caminhoArquivo}");

        try
        {
            Process.Start(new ProcessStartInfo(caminhoArquivo) { UseShellExecute = true });
        }
        catch
        {
            // Caso não abra o leitor de PDF automaticamente
        }
    }

    private static byte[] GerarBytesQrCode(string conteudo)
    {
        using QRCodeGenerator qrGenerator = new QRCodeGenerator();
        using QRCodeData qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q);
        using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        return qrCode.GetGraphic(20);
    }
}