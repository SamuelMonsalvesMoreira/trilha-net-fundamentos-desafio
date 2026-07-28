namespace DesafioFundamentos.Services;

using DesafioFundamentos.Models.Veiculos;

public class RelatorioService
{
    public void ExibirQuantidade(List<Veiculo> veiculos)
    {
        Console.WriteLine($"Veículos estacionados: {veiculos.Count}");
    }
}