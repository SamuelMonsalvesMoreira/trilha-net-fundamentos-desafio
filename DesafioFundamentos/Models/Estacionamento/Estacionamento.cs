namespace DesafioFundamentos.Models.Estacionamento;

public class Estacionamento
{
    public decimal PrecoInicial { get; }

    public decimal PrecoHora { get; }

    public Estacionamento(decimal precoInicial, decimal precoHora)
    {
        PrecoInicial = precoInicial;

        PrecoHora = precoHora;
    }
}