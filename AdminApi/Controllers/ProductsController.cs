using AdminApi.Requests;
using AdminApi.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
    {
        var createModel = request.ToProductCreateModel();

        var result = await _productService.CreateProductAsync(createModel);

        //FIX ALL THIS WHEN U KNOW HOW TO HANDLE BETTER
        if (!result.Success) return BadRequest();

        var product = result.Value;

        return CreatedAtAction(nameof(Get), new { id = product?.Id }, product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product.ToProductResponse());
    }
}