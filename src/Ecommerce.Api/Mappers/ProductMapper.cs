namespace Ecommerce.Api.Mappers;

public static class ProductMapper
{
    public static SearchProductResponse ToResponse(this Product product)
    {
        return new SearchProductResponse(product.Id, product.Title, product.CurrentPrice);
    }
}