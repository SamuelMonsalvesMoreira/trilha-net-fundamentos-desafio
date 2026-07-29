using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;

namespace DesafioFundamentos.Services;

public class RelatorioService
{
    public void ExibirTicket(Ticket ticket)
    {
        Console.Clear();

        Console.WriteLine("======================================");
        Console.WriteLine("      TICKET DE ESTACIONAMENTO");
        Console.WriteLine("======================================");

        Console.WriteLine($"Número      : {ticket.Numero}");
        Console.WriteLine($"Placa       : {ticket.Placa}");
        Console.WriteLine($"Entrada     : {ticket.Entrada:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"Saída       : {ticket.Saida:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"Tempo       : {ticket.Permanencia:hh\\:mm\\:ss}");
        Console.WriteLine($"Valor Pago  : R$ {ticket.ValorPago:F2}");

        Console.WriteLine("======================================");
    }

    public void ExibirQuantidade(List<Veiculo> veiculos)
    {
        Console.WriteLine();
        Console.WriteLine($"Total de veículos: {veiculos.Count}");
    }

    public void ExibirVeiculos(List<Veiculo> veiculos)
    {
        if (!veiculos.Any())
        {
            Console.WriteLine("Nenhum veículo estacionado.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== VEÍCULOS ESTACIONADOS =====");
        Console.WriteLine();

        foreach (Veiculo veiculo in veiculos)
        {
            TimeSpan tempo = DateTime.Now - veiculo.Entrada;

            Console.WriteLine($"Placa   : {veiculo.Placa}");
            Console.WriteLine($"Entrada : {veiculo.Entrada:dd/MM/yyyy HH:mm:ss}");
            Console.WriteLine($"Tempo   : {tempo:hh\\:mm\\:ss}");
            Console.WriteLine("--------------------------------");
        }
    }
}