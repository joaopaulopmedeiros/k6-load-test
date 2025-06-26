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
            .From((request.Page - 1) * request.Size)
            .Size(request.Size)
            .Query(q => q.Bool(b =>
            {
                var mustQueries = new List<Query>();

                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    mustQueries.Add(new MatchQuery
                    {
                        Field = "title",
                        Query = request.Title
                    });
                }

                b.Must(mustQueries);
            }))
        );

        return response.Documents;
    }

}
