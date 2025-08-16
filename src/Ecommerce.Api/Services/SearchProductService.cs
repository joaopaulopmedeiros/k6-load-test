using Microsoft.Extensions.Caching.Distributed;

using System.Text.Json;
namespace Ecommerce.Api.Services;

public class SearchProductService(ElasticsearchClient client, IDistributedCache cache) : ISearchProductService
{
    public async Task<IEnumerable<Product>> SearchAsync(SearchProductRequest request)
    {
        string cacheKey = $"products:{request.Title}:{request.Page}:{request.Size}";

        string? cached = await cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cached))
        {
            return JsonSerializer.Deserialize<IEnumerable<Product>>(cached) ?? [];
        }

        SearchResponse<Product> response = await client.SearchAsync<Product>(s => s
            .Indices(ProductIndex.Name)
            .From((request.Page - 1) * request.Size)
            .Size(request.Size)
            .Query(q => q.Bool(b =>
            {
                List<Query> mustQueries = [];

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

        var products = response.Documents;

        if (products != null && products.Count != 0)
        {
            await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(products), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        return products ?? [];
    }
}
