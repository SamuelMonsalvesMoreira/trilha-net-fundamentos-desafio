using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Interfaces;

namespace DesafioFundamentos.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly List<Veiculo> veiculos = new();

    public void Adicionar(Veiculo veiculo)
    {
        veiculos.Add(veiculo);
    }

    public bool Remover(Veiculo veiculo)
    {
        return veiculos.Remove(veiculo);
    }

    public List<Veiculo> Listar()
    {
        return veiculos;
    }

    public Veiculo? BuscarPorPlaca(string placa)
    {
        return veiculos.FirstOrDefault(v =>
            v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }

    public bool Existe(string placa)
    {
        return veiculos.Any(v =>
            v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }

    public int Quantidade()
    {
        return veiculos.Count;
    }
}