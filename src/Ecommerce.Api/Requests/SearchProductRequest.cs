namespace Ecommerce.Api.Requests;

public record SearchProductRequest(string? Title, int Sku, int Page = 1, int Size = 10);