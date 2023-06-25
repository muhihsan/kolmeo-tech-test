using API.Dto;
using API.Model;
using API.Repositories;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<PaginatedItemsViewModel<Product>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var counts = await _productRepository.Count();
        var products = await _productRepository.GetAllAsync(pageSize, pageIndex);

        return Ok(new PaginatedItemsViewModel<Product>(pageSize, pageIndex, counts, products));
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<Product>> Create([FromBody] Product product)
    {
        var result = await _productRepository.CreateAsync(product);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult<Product>> Update(Guid id, [FromBody] ProductUpdateDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return BadRequest();
        }

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;

        await _productRepository.UpdateAsync(product);

        return Ok(product);
    }
}
