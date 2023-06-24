using API.Dto;
using API.Model;
using API.Repositories;
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
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _productRepository.GetAllAsync());
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult> Create([FromBody] Product product)
    {
        var result = await _productRepository.CreateAsync(product);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(Guid id)
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
    public async Task<ActionResult> Update(Guid id, [FromBody] ProductUpdateDto productDto)
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
