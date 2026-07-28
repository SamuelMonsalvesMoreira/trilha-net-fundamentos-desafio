using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Utils;

namespace DesafioFundamentos.Services;

public class EstacionamentoService
{
    private readonly Estacionamento estacionamento;
    private readonly List<Veiculo> veiculos;
    private readonly PagamentoService pagamentoService;

    public EstacionamentoService(Estacionamento estacionamento)
    {
        this.estacionamento = estacionamento;

        pagamentoService = new PagamentoService();

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

        Console.WriteLine("\nVeículo cadastrado com sucesso!");
        Console.WriteLine($"Placa: {veiculo.Placa}");
        Console.WriteLine($"Entrada: {veiculo.Entrada:dd/MM/yyyy HH:mm:ss}");
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

        TimeSpan tempo = DateTime.Now - veiculo.Entrada;

        veiculos.Remove(veiculo);

        Console.WriteLine("\nVeículo removido com sucesso!");
        Console.WriteLine($"Tempo estacionado: {tempo:hh\\:mm\\:ss}");
        Console.WriteLine($"Valor total: R$ {valor:F2}");
    }

    public void ListarVeiculos()
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

    private Veiculo? BuscarVeiculo(string placa)
    {
        return veiculos.FirstOrDefault(v =>
            v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }
}