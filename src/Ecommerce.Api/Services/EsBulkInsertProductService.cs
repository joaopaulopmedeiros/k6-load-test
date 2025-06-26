namespace Ecommerce.Api.Services;

public class EsBulkInsertProductService(ElasticsearchClient client) : IBulkInsertProductService
{
    public async Task BulkInsertAsync(int quantity = 1000)
    {
        List<Product> products = Enumerable.Range(1, quantity).Select(i => new Product()
        {
            Id = Guid.NewGuid(),
            Title = $"Product {i}",
            Sku = 52003892 + i,
            CurrentPrice = 950.99m,
            OriginalPrice = 1000.00m,
            Quantity = 10,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        }).ToList();

        BulkRequest bulkRequest = new(ProductIndex.Name)
        {
            Operations = new List<IBulkOperation>()
        };

        foreach (var product in products)
        {
            bulkRequest.Operations.Add(new BulkIndexOperation<Product>(product));
        }

        BulkResponse response = await client.BulkAsync(bulkRequest);

        if (!response.IsValidResponse)
        {
            throw new Exception($"Bulk insert failed: {response.DebugInformation}");
        }
    }
}
