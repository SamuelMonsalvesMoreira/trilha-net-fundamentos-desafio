using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models.Veiculos;
using DesafioFundamentos.Persistence;

namespace DesafioFundamentos.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly JsonStorage storage;
    private readonly FileManager fileManager;
    private readonly List<Veiculo> veiculos;

    public VeiculoRepository()
    {
        storage = new JsonStorage();
        fileManager = new FileManager();

        fileManager.GarantirArquivo(StoragePaths.Veiculos);

        veiculos = storage.Carregar<Veiculo>(StoragePaths.Veiculos);
    }

    public void Adicionar(Veiculo veiculo)
    {
        veiculos.Add(veiculo);
        Salvar();
    }

    public bool Remover(Veiculo veiculo)
    {
        bool removido = veiculos.Remove(veiculo);

        if (removido)
        {
            Salvar();
        }

        return removido;
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

    private void Salvar()
    {
        storage.Salvar(StoragePaths.Veiculos, veiculos);
    }
}