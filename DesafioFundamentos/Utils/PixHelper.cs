using System.Text;

namespace DesafioFundamentos.Utils;

public static class PixHelper
{
    public static string GerarPayloadStatic(string chavePix, string nomeRecebedor, string cidade, decimal valor, string txId = "***")
    {
        // Trata os dados de acordo com as regras do Banco Central
        nomeRecebedor = RemoverAcentos(nomeRecebedor).ToUpper();
        cidade = RemoverAcentos(cidade).ToUpper();

        if (nomeRecebedor.Length > 25) nomeRecebedor = nomeRecebedor.Substring(0, 25);
        if (cidade.Length > 15) cidade = cidade.Substring(0, 15);

        string valorFormatado = valor.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);

        // Estrutura EMVCo / BR Code do Pix
        string gui = FormatarCampo("00", "br.gov.bcb.pix");
        string chave = FormatarCampo("01", chavePix);
        string merchantAccount = FormatarCampo("26", gui + chave);

        StringBuilder sb = new StringBuilder();
        sb.Append(FormatarCampo("00", "01")); // Payload Format Indicator
        sb.Append(merchantAccount);           // Dados do recebedor Pix
        sb.Append(FormatarCampo("52", "0000")); // Merchant Category Code
        sb.Append(FormatarCampo("53", "986"));  // Moeda (986 = BRL)
        sb.Append(FormatarCampo("54", valorFormatado)); // Valor a pagar
        sb.Append(FormatarCampo("58", "BR"));   // Código do país
        sb.Append(FormatarCampo("59", nomeRecebedor)); // Nome do titular
        sb.Append(FormatarCampo("60", cidade)); // Cidade do titular

        // Dados adicionais (TxID)
        string campoTxId = FormatarCampo("05", string.IsNullOrWhiteSpace(txId) ? "***" : txId);
        sb.Append(FormatarCampo("62", campoTxId));

        // Prefixo do campo de verificação CRC16
        sb.Append("6304");

        // Cálculo final do CRC16
        string payloadSemCrc = sb.ToString();
        string crc = CalcularCrc16(payloadSemCrc);

        return payloadSemCrc + crc;
    }

    private static string FormatarCampo(string id, string valor)
    {
        string tamanho = valor.Length.ToString("D2");
        return $"{id}{tamanho}{valor}";
    }

    private static string RemoverAcentos(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return "";
        string normalizado = texto.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        foreach (char c in normalizado)
        {
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

    private static string CalcularCrc16(string payload)
    {
        ushort crc = 0xFFFF;
        ushort polynomial = 0x1021;
        byte[] bytes = Encoding.UTF8.GetBytes(payload);

        foreach (byte b in bytes)
        {
            for (int i = 0; i < 8; i++)
            {
                bool bit = ((b >> (7 - i)) & 1) == 1;
                bool c15 = ((crc >> 15) & 1) == 1;
                crc <<= 1;
                if (c15 ^ bit) crc ^= polynomial;
            }
        }

        return (crc & 0xFFFF).ToString("X4");
    }
}