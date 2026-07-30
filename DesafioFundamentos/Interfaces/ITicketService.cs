using DesafioFundamentos.Models.Estacionamento;

namespace DesafioFundamentos.Interfaces; 

public interface ITicketService
{
    Ticket GerarTicket(
        string placa,
        DateTime entrada,
        DateTime saida,
        decimal valorPago);
}