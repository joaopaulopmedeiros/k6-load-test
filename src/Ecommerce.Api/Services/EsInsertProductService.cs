using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;

namespace Ecommerce.Api.Services;

public class EsAddProductService : IAddProductService
{
    private readonly ElasticsearchClient _client;

    public EsAddProductService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task AddAsync(Product product)
    {
        var response = await _client.IndexAsync(product, x => x.Index("products"));

        if (!response.IsValidResponse)
        {
            Console.WriteLine($"Index document with ID {response.Id} failed {response.DebugInformation}");
        }
    }
}
