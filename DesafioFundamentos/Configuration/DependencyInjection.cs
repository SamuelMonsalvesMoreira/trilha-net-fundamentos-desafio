using DesafioFundamentos.Interfaces;
using DesafioFundamentos.Repositories;
using DesafioFundamentos.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioFundamentos.Configuration;

public static class DependencyInjection
{
    public static ServiceProvider Configurar()
    {
        ServiceCollection services = new();

        // Repositórios
        services.AddSingleton<IVeiculoRepository, VeiculoRepository>();

        // Serviços
        services.AddSingleton<IPagamentoService, PagamentoService>();
        services.AddSingleton<ITicketService, TicketService>();
        services.AddSingleton<IRelatorioService, RelatorioService>();

        return services.BuildServiceProvider();
    }
}