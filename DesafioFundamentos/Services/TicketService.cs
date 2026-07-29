using DesafioFundamentos.Models.Estacionamento;

namespace DesafioFundamentos.Services;

public class TicketService
{
    private int numeroTicket = 1;

    public Ticket GerarTicket(
        string placa,
        DateTime entrada,
        DateTime saida,
        decimal valor)
    {
        return new Ticket(
            numeroTicket++,
            placa,
            entrada,
            saida,
            valor);
    }
}