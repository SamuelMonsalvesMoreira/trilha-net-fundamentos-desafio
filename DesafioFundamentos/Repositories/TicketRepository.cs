using DesafioFundamentos.Models.Estacionamento;
using DesafioFundamentos.Persistence;

namespace DesafioFundamentos.Repositories;


public class TicketRepository
{
    private readonly JsonStorage _jsonStorage;

    public TicketRepository()
    {
        _jsonStorage = new JsonStorage();
    }

    public void Adicionar(Ticket ticket)
    {
        List<Ticket> tickets = _jsonStorage.Carregar<Ticket>(StoragePaths.Tickets);
        tickets.Add(ticket);
        _jsonStorage.Salvar(StoragePaths.Tickets, tickets);
    }

    public List<Ticket> Listar()
    {
        return _jsonStorage.Carregar<Ticket>(StoragePaths.Tickets);
    }

    public int Quantidade()
    {
        return Listar().Count;
    }
}