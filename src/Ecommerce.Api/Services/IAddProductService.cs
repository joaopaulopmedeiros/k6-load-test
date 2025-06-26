namespace Ecommerce.Api.Services;

public interface IAddProductService
{
    Task AddAsync(Product product);
}