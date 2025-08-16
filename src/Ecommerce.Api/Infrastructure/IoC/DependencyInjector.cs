namespace Ecommerce.Api.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
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

        services.AddSingleton<ISearchProductService, SearchProductService>();

        return services;
    }
}
