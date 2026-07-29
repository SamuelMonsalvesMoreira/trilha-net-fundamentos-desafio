using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Models.Veiculos;

namespace DesafioFundamentos.Interfaces;

public interface IRelatorioService
{
    void ExibirVeiculoCadastrado(Veiculo veiculo);

    void ExibirVeiculoDuplicado();

    void ExibirVeiculoNaoEncontrado();

    void ExibirTicket(Ticket ticket);

    void ExibirListaVeiculos(List<Veiculo> veiculos);
}