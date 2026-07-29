namespace DesafioFundamentos.Models.Estacionamento;

public class Ticket
{
    public int Numero { get; }

    public string Placa { get; }

    public DateTime Entrada { get; }

    public DateTime Saida { get; }

    public TimeSpan Permanencia { get; }

    public decimal ValorPago { get; }

    public Ticket(
        int numero,
        string placa,
        DateTime entrada,
        DateTime saida,
        decimal valorPago)
    {
        Numero = numero;
        Placa = placa;
        Entrada = entrada;
        Saida = saida;
        ValorPago = valorPago;

        Permanencia = saida - entrada;
    }
}