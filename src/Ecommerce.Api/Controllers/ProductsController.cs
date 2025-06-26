namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> SearchAsync
    (
        [FromQuery] SearchProductRequest request,
        [FromServices] ISearchProductService service
    )
    {
        IEnumerable<Product> products = await service.SearchAsync(request);
        return products is null || !products.Any() ? NoContent() : Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync
    (
        [FromBody] Product product,
        [FromServices] IAddProductService service
    )
    {
        await service.AddAsync(product);
        return Created();
    }
}
