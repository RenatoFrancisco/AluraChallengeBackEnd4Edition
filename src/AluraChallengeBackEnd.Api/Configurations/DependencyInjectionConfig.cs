namespace AluraChallengeBackEnd.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddScoped<INotifier, Notifier>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IExpenditureService, ExpenditureService>();
        services.AddScoped<ISummaryService, SummaryService>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<IExpenditureRepository, ExpenditureRepository>();

        return services;
    }
}