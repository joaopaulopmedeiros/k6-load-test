namespace Ecommerce.Api.Responses;

public record SearchProductResponse(
    Guid Id,
    string? Title,
    decimal Price
);