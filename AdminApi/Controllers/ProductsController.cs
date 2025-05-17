using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(ProductMapper.MapProduct(product));
    }
}