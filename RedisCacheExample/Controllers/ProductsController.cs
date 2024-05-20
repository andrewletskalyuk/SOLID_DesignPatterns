using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Services;

namespace RedisCacheExample.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    readonly ProductService _productService;

	public ProductsController(ProductService productService)
	{
		_productService = productService;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductById(int id)
	{
		var product = await _productService.GetProductByIdAsync(id);

		if (product == null)
		{
			return NotFound();
		}

		return Ok(product);
	}
}
