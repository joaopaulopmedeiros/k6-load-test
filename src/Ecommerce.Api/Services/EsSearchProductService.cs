using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Ecommerce.Api.Services;

public class EsSearchProductService : ISearchProductService
{
    private readonly ElasticsearchClient _client;
    private const string IndexName = "products";

    public EsSearchProductService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Product>> SearchAsync(SearchProductRequest request)
    {
        var response = await _client.SearchAsync<Product>(s => s
            .Indices("products")
            .From(0)
            .Size(10)
        );

        return response.Documents;
    }
}
