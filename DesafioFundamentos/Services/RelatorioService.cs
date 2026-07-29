using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;

namespace DesafioFundamentos.Services;

public class RelatorioService : IRelatorioService
{
    public void ExibirVeiculoCadastrado(Veiculo veiculo)
    {
        Console.WriteLine("\nVeículo cadastrado com sucesso!");
        Console.WriteLine($"Placa: {veiculo.Placa}");
        Console.WriteLine($"Entrada: {veiculo.Entrada:dd/MM/yyyy HH:mm:ss}");
    }

    public void ExibirVeiculoDuplicado()
    {
        Console.WriteLine("\nEste veículo já está estacionado.");
    }

    public void ExibirVeiculoNaoEncontrado()
    {
        Console.WriteLine("\nVeículo não encontrado.");
    }

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
        Console.WriteLine($"Permanência : {ticket.Permanencia:hh\\:mm\\:ss}");
        Console.WriteLine($"Valor Pago  : R$ {ticket.ValorPago:F2}");
        Console.WriteLine("======================================");
    }

    public void ExibirListaVeiculos(List<Veiculo> veiculos)
    {
        if (!veiculos.Any())
        {
            Console.WriteLine("Nenhum veículo estacionado.");
            return;
        }

        Console.WriteLine("\n==== VEÍCULOS ESTACIONADOS ====\n");

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