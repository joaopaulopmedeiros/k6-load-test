using Elastic.Clients.Elasticsearch;

namespace Ecommerce.Api.Infrastructure.IoC;

public static class DependencyInjector
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSingleton(s =>
        {
            var configuration = s.GetRequiredService<IConfiguration>();

            var settings = new ElasticsearchClientSettings(new Uri(configuration["ES_CONNECTION"]!))
                .DefaultIndex("products")
                .EnableDebugMode();

            return new ElasticsearchClient(settings);
        });

        services.AddSingleton<ISearchProductService, EsSearchProductService>();

        services.AddSingleton<IAddProductService, EsAddProductService>();

        return services;
    }
}
