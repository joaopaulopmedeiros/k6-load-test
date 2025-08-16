namespace Ecommerce.Api.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ISearchProductService, SearchProductService>();
        return services;
    }

    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(s =>
        {
            IConfiguration configuration = s.GetRequiredService<IConfiguration>();

            Uri uri = new(configuration["ES_CONNECTION"]!);

            ElasticsearchClientSettings settings = new ElasticsearchClientSettings(uri)
                .DefaultIndex(ProductIndex.Name)
                .EnableDebugMode();

            return new ElasticsearchClient(settings);
        });

        services.AddSingleton<ProductSeeder>();

        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["REDIS_CONNECTION"];
            options.InstanceName = "ecommerce-api:";
        });

        return services;
    }
}
