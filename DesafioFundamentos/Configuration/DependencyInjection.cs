using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Repositories;
using DesafioFundamentos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioFundamentos.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddSingleton<IVeiculoRepository, VeiculoRepository>();

        services.AddSingleton<IPagamentoService, PagamentoService>();

        services.AddSingleton<ITicketService, TicketService>();

        services.AddSingleton<IRelatorioService, RelatorioService>();

        return services;
    }
}