namespace Ecommerce.Api.Services;

public interface ISearchProductService
{
    public Task<IEnumerable<SearchProductResponse>> SearchAsync(SearchProductRequest request);
}
