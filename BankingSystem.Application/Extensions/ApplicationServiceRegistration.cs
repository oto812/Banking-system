using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAtmService, AtmService>();
        services.AddScoped<IOperatorService, OperatorService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IUserService,UserService>();
        return services;
    }
}