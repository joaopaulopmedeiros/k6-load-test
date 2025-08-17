namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SearchAsync
    (
        [FromQuery] SearchProductRequest request,
        [FromServices] ISearchProductService service
    )
    {
        IEnumerable<SearchProductResponse> products = await service.SearchAsync(request);
        return products is null || !products.Any() ? NoContent() : Ok(products);
    }
}