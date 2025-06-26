namespace Ecommerce.Api.Services;

public class EsSearchProductService(ElasticsearchClient client) : ISearchProductService
{
    public async Task<IEnumerable<Product>> SearchAsync(SearchProductRequest request)
    {
        var response = await client.SearchAsync<Product>(s => s
            .Indices(ProductIndex.Name)
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
