using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Utils;

namespace DesafioFundamentos.Services;

public class EstacionamentoService
{
    private readonly Estacionamento estacionamento;
    private readonly List<Veiculo> veiculos;

    private readonly PagamentoService pagamentoService;
    private readonly TicketService ticketService;
    private readonly RelatorioService relatorioService;

   public EstacionamentoService(
    Estacionamento estacionamento,
    PagamentoService pagamentoService,
    TicketService ticketService,
    RelatorioService relatorioService)
{
    this.estacionamento = estacionamento;
    this.pagamentoService = pagamentoService;
    this.ticketService = ticketService;
    this.relatorioService = relatorioService;

    veiculos = new List<Veiculo>();
}

    public void AdicionarVeiculo()
    {
        string placa = ConsoleHelper.LerTexto("Digite a placa: ").ToUpper();

        if (veiculos.Any(v => v.Placa == placa))
        {
            Console.WriteLine("Este veículo já está estacionado.");
            return;
        }

        Veiculo veiculo = new Veiculo(placa);

        veiculos.Add(veiculo);

        Console.WriteLine();
        Console.WriteLine("Veículo cadastrado com sucesso!");
        Console.WriteLine($"Placa   : {veiculo.Placa}");
        Console.WriteLine($"Entrada : {veiculo.Entrada:dd/MM/yyyy HH:mm:ss}");
    }

    public void RemoverVeiculo()
    {
        string placa = ConsoleHelper.LerTexto("Digite a placa: ").ToUpper();

        Veiculo? veiculo = BuscarVeiculo(placa);

        if (veiculo == null)
        {
            Console.WriteLine("Veículo não encontrado.");
            return;
        }

        decimal valor = pagamentoService.CalcularValor(
            estacionamento.PrecoInicial,
            estacionamento.PrecoHora,
            veiculo.Entrada);

        DateTime saida = DateTime.Now;

        Ticket ticket = ticketService.GerarTicket(
            veiculo.Placa,
            veiculo.Entrada,
            saida,
            valor);

        veiculos.Remove(veiculo);

        relatorioService.ExibirTicket(ticket);
    }

    public void ListarVeiculos()
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

        relatorioService.ExibirQuantidade(veiculos);
    }

    private Veiculo? BuscarVeiculo(string placa)
    {
        return veiculos.FirstOrDefault(v =>
            v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }
}