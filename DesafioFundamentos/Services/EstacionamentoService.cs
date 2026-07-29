using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Utils;

namespace DesafioFundamentos.Services;

public class EstacionamentoService
{
    private readonly Estacionamento estacionamento;
    private readonly IVeiculoRepository repositorio;
    private readonly IPagamentoService pagamentoService;
    private readonly ITicketService ticketService;
    private readonly IRelatorioService relatorioService;

    public EstacionamentoService(
        Estacionamento estacionamento,
        IVeiculoRepository repositorio,
        IPagamentoService pagamentoService,
        ITicketService ticketService,
        IRelatorioService relatorioService)
    {
        this.estacionamento = estacionamento;
        this.repositorio = repositorio;
        this.pagamentoService = pagamentoService;
        this.ticketService = ticketService;
        this.relatorioService = relatorioService;
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

        repositorio.Remover(veiculo);

        relatorioService.ExibirTicket(ticket);
    }

    public void ListarVeiculos()
    {
        relatorioService.ExibirListaVeiculos(
            repositorio.Listar());
    }
}