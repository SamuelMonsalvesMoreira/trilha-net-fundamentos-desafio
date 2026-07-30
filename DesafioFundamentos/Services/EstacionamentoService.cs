using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Repositories;
using DesafioFundamentos.Utils;

namespace DesafioFundamentos.Services;

public class EstacionamentoService
{
    private readonly Estacionamento estacionamento;
    private readonly IVeiculoRepository repositorio;
    private readonly IPagamentoService pagamentoService;
    private readonly TicketService ticketService;
    private readonly IRelatorioService relatorioService;
    private readonly TicketRepository ticketRepository; 
    public EstacionamentoService(
        Estacionamento estacionamento,
        IVeiculoRepository repositorio,
        IPagamentoService pagamentoService,
        TicketService ticketService, 
        IRelatorioService relatorioService,
        TicketRepository ticketRepository)
    {
        this.estacionamento = estacionamento;
        this.repositorio = repositorio;
        this.pagamentoService = pagamentoService;
        this.ticketService = ticketService;
        this.relatorioService = relatorioService;
        this.ticketRepository = ticketRepository;
    }

    public void AdicionarVeiculo()
    {
        string placa = ConsoleHelper
            .LerTexto("Digite a placa: ")
            .ToUpper();

        if (repositorio.Existe(placa))
        {
            relatorioService.ExibirVeiculoDuplicado();
            return;
        }

        Veiculo veiculo = new Veiculo(placa);

        repositorio.Adicionar(veiculo);

        relatorioService.ExibirVeiculoCadastrado(veiculo);
    }

    public void RemoverVeiculo()
{
    string placa = ConsoleHelper
        .LerTexto("Digite a placa: ")
        .ToUpper();

    Veiculo? veiculo = repositorio.BuscarPorPlaca(placa);

    if (veiculo is null)
    {
        relatorioService.ExibirVeiculoNaoEncontrado();
        return;
    }

    decimal valor = pagamentoService.CalcularValor(
        estacionamento.PrecoInicial,
        estacionamento.PrecoHora,
        veiculo.Entrada);

    Ticket ticket = ticketService.GerarTicket(
        veiculo.Placa,
        veiculo.Entrada,
        DateTime.Now,
        valor);

    ticketRepository.Adicionar(ticket);

    bool removido = repositorio.Remover(veiculo);

    if (!removido)
    {
        Console.WriteLine("Erro ao remover o veículo.");
        return;
    }

    // Exibe no console
    relatorioService.ExibirTicket(ticket);

    // 👈 GERA O PDF E ABRE O COMPROVANTE COM QR CODE AUTOMATICAMENTE
    PdfService.GerarComprovantePdf(ticket);
}

    public void ListarVeiculos()
    {
        relatorioService.ExibirListaVeiculos(
            repositorio.Listar());
    }
}