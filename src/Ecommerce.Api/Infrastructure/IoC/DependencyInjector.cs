namespace Ecommerce.Api.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ISearchProductService, SearchProductService>();
        return services;
    }

    public static IServiceCollection AddMySqlDatabase(this IServiceCollection services)
    {
        MySqlServerVersion serverVersion = new(new Version(8, 0, 34));
        string? connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

        services.AddDbContext<EcommerceContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );

        services
            .AddHealthChecks()
            .AddDbContextCheck<EcommerceContext>();

        return services;
    }
}
