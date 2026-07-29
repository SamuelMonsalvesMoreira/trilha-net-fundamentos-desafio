using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Services;
using DesafioFundamentos.Utils;

Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = ConsoleHelper.LerDecimal("Preço inicial: ");
decimal precoHora = ConsoleHelper.LerDecimal("Preço por hora: ");

Estacionamento estacionamento = new(precoInicial, precoHora);

PagamentoService pagamentoService = new();
TicketService ticketService = new();
RelatorioService relatorioService = new();

EstacionamentoService service = new(
    estacionamento,
    pagamentoService,
    ticketService,
    relatorioService);

bool executar = true;

while (executar)
{
    ExibirMenu();

    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            service.AdicionarVeiculo();
            break;

        case "2":
            service.RemoverVeiculo();
            break;

        case "3":
            service.ListarVeiculos();
            break;

        case "4":
            executar = false;
            continue;

        default:
            Console.WriteLine("\nOpção inválida!");
            break;
    }

    ConsoleHelper.Pausar();
}

Console.WriteLine("\nPrograma encerrado!");

static void ExibirMenu()
{
    Console.Clear();

    Console.WriteLine("==================================");
    Console.WriteLine("      SISTEMA DE ESTACIONAMENTO");
    Console.WriteLine("==================================");
    Console.WriteLine("1 - Adicionar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Sair");
    Console.Write("\nEscolha uma opção: ");
}