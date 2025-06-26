namespace Ecommerce.Api.Services;

public interface IBulkInsertProductService
{
    Task BulkInsertAsync(int quantity = 1000);
}