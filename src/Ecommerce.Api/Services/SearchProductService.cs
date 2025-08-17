namespace Ecommerce.Api.Services;

public class SearchProductService(ElasticsearchClient client, IDistributedCache cache) : ISearchProductService
{
    public async Task<IEnumerable<SearchProductResponse>> SearchAsync(SearchProductRequest request)
    {
        string cacheKey = GenerateCacheKey(request);

        IEnumerable<Product>? cachedProducts = await GetFromCacheAsync(cacheKey);

        if (cachedProducts is not null) return cachedProducts.Select(p => p.ToResponse());

        IEnumerable<Product> products = await SearchProductsAsync(request);

        if (products.Any()) await SetCacheAsync(cacheKey, products);

        return products.Select(p => p.ToResponse());
    }

    private static string GenerateCacheKey(SearchProductRequest request) =>
        $"products:{request.Title}:{request.Page}:{request.Size}";

    private async Task<IEnumerable<Product>?> GetFromCacheAsync(string cacheKey)
    {
        string? cached = await cache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(cached)
            ? null
            : JsonSerializer.Deserialize<IEnumerable<Product>>(cached);
    }

    private async Task<IEnumerable<Product>> SearchProductsAsync(SearchProductRequest request)
    {
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

        return response.Documents ?? [];
    }

    private async Task SetCacheAsync(string cacheKey, IEnumerable<Product> products)
    {
        DistributedCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(products), options);
    }
}
