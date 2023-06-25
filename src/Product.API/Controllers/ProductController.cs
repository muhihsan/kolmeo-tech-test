using System.Net;
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
    // TODO: Add logs
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(PaginatedItemsViewModel<ProductDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<PaginatedItemsViewModel<ProductDto>>> GetAll([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        if (pageSize < 1 || pageIndex < 0)
        {
            // TODO: Return the reason
            return BadRequest();
        }

        var counts = await _productRepository.Count();
        var products = await _productRepository.GetAllAsync(pageSize, pageIndex);

        // TODO: Use Automapper
        var productsDto = products.Select(product => new ProductDto(product));

        return Ok(new PaginatedItemsViewModel<ProductDto>(pageSize, pageIndex, counts, productsDto));
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductDto product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // TODO: Use Automapper
        var result = await _productRepository.CreateAsync(
            new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            });

        // TODO: Use Automapper
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ProductDto(result));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        // TODO: Use Automapper
        return Ok(new ProductDto(product));
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] ProductUpdateDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        // TODO: Use Automapper
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;

        await _productRepository.UpdateAsync(product);

        // TODO: Use Automapper
        return Ok(new ProductDto(product));
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        await _productRepository.DeleteAsync(product);

        return NoContent();
    }
}
