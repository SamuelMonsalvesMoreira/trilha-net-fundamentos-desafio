namespace DesafioFundamentos.Interfaces;

public interface IPagamentoService
{
    decimal CalcularValor(
        decimal precoInicial,
        decimal precoHora,
        DateTime entrada);
}