using DesafioFundamentos.Models.Veiculos;

namespace DesafioFundamentos.Interfaces;

public interface IVeiculoRepository
{
    void Adicionar(Veiculo veiculo);

    bool Remover(Veiculo veiculo);

    List<Veiculo> Listar();

    Veiculo? BuscarPorPlaca(string placa);

    bool Existe(string placa);

    int Quantidade();
}