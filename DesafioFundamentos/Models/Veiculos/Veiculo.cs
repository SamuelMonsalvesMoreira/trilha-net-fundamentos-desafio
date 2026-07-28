namespace DesafioFundamentos.Models.Veiculos;

public class Veiculo
{
    public string Placa { get; }

    public DateTime Entrada { get; }

    public Veiculo(string placa)
    {
        Placa = placa.Trim().ToUpper();

        Entrada = DateTime.Now;
    }
}