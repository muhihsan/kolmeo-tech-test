using API.Dto;
using API.Model;
using API.Repositories;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Controllers;

public class ProductControllerTests
{
    private Mock<IProductRepository> _mockProductRepository;
    private Mock<ILogger<ProductController>> _mockLogger;
    private ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockLogger = new Mock<ILogger<ProductController>>();
        _controller = new ProductController(_mockProductRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_WhenSuccessful_ShouldReturnOkAndAllProducts()
    {
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 }
        };

        _mockProductRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

        var result = await _controller.GetAll();

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, ((OkObjectResult)result).Value);
    }

    [Fact]
    public async Task Create_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 };

        _mockProductRepository.Setup(x => x.CreateAsync(It.IsAny<Product>())).ReturnsAsync(product);

        var result = await _controller.Create(product);

        Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(product, ((CreatedAtActionResult)result).Value);
    }

    [Fact]
    public async Task GetById_WhenNotFound_ShouldReturnNotFound()
    {
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var result = await _controller.GetById(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetById_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 };

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

        var result = await _controller.GetById(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, ((OkObjectResult)result).Value);
    }

    [Fact]
    public async Task Update_WhenNotFound_ShouldReturnBadRequest()
    {
        var productUpdateDto = new ProductUpdateDto { Name = "Test Name", Description = "Test Description", Price = 5 };

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var result = await _controller.Update(Guid.NewGuid(), productUpdateDto);

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Update_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Test Name", Description = "Test Description", Price = 5 };
        var productUpdateDto = new ProductUpdateDto { Name = "Test New Name", Description = "Test New Description", Price = 10 };

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

        var result = await _controller.Update(productId, productUpdateDto);

        Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, ((OkObjectResult)result).Value);
    }
}

