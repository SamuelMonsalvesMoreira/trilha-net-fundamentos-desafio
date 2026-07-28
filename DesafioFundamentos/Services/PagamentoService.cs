namespace DesafioFundamentos.Services;

public class PagamentoService
{
    public decimal CalcularValor(decimal precoInicial,
                                 decimal precoHora,
                                 DateTime entrada)
    {
        TimeSpan permanencia = DateTime.Now - entrada;

        int horas = (int)Math.Ceiling(permanencia.TotalHours);

        return precoInicial + (horas * precoHora);
    }
}