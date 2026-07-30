using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Repositories;

namespace DesafioFundamentos.Services;

public class TicketService : ITicketService
{
    private int numeroTicket;

    public TicketService(TicketRepository ticketRepository)
    {
        List<Ticket> tickets = ticketRepository.Listar();

        numeroTicket = tickets.Any()
            ? tickets.Max(t => t.Numero) + 1
            : 1;
    }

    public Ticket GerarTicket(
        string placa,
        DateTime entrada,
        DateTime saida,
        decimal valorPago)
    {
        return new Ticket(
            numeroTicket++,
            placa,
            entrada,
            saida,
            valorPago);
    }
}