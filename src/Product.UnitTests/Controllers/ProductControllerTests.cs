using API.Dto;
using API.Model;
using API.Repositories;
using API.ViewModel;
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

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(1, -1)]
    [InlineData(0, 0)]
    public async Task GetAll_WhenInvalidPageSizeOrPageIndexPassed_ShouldReturnBadRequest(int pageSize, int pageIndex)
    {
        var result = await _controller.GetAll(pageSize, pageIndex);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task GetAll_WhenNoPageSizeAndPageIndexPassed_ShouldReturnOkAndDefaultPaginatedProducts()
    {
        var defaultPageSize = 10;
        var defaultPageIndex = 0;
        var count = 5;
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 }
        };
        var expectedResult = new PaginatedItemsViewModel<ProductDto>(
            defaultPageSize, defaultPageIndex, count, products.Select(product => new ProductDto(product)));

        _mockProductRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(products);
        _mockProductRepository.Setup(x => x.Count()).ReturnsAsync(5);

        var result = await _controller.GetAll();

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equivalent(expectedResult, ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public async Task GetAll_WhenPageSizeAndPageIndexPassed_ShouldReturnOkAndPaginatedProducts()
    {
        var pageSize = 1;
        var pageIndex = 1;
        var count = 5;
        var products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 }
        };
        var expectedResult = new PaginatedItemsViewModel<Product>(pageSize, pageIndex, count, products);

        _mockProductRepository.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(products);
        _mockProductRepository.Setup(x => x.Count()).ReturnsAsync(5);

        var result = await _controller.GetAll(pageSize, pageIndex);

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equivalent(expectedResult, ((OkObjectResult)result.Result).Value);
    }

    // TODO: Add test Create_WhenModelStateIsInvalid_ShouldReturnBadRequest

    [Fact]
    public async Task Create_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 };
        var productDto = new ProductDto(product);

        _mockProductRepository.Setup(x => x.CreateAsync(It.IsAny<Product>())).ReturnsAsync(product);

        var result = await _controller.Create(productDto);

        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equivalent(productDto, ((CreatedAtActionResult)result.Result).Value);
    }

    [Fact]
    public async Task GetById_WhenNotFound_ShouldReturnNotFound()
    {
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var result = await _controller.GetById(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var product = new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 5 };
        var expectedResult = new ProductDto(product);

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

        var result = await _controller.GetById(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equivalent(expectedResult, ((OkObjectResult)result.Result).Value);
    }

    // TODO: Add test Update_WhenModelStateIsInvalid_ShouldReturnBadRequest

    [Fact]
    public async Task Update_WhenNotFound_ShouldReturnNotFound()
    {
        var productUpdateDto = new ProductUpdateDto("Test Name", "Test Description", 5);

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var result = await _controller.Update(Guid.NewGuid(), productUpdateDto);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Update_WhenSuccessful_ShouldReturnOkAndProduct()
    {
        var productId = Guid.NewGuid();
        var oldProduct = new Product { Id = productId, Name = "Test Name", Description = "Test Description", Price = 5 };
        var productUpdateDto = new ProductUpdateDto("Test New Name", "Test New Description", 10);
        var expectedResult = new ProductDto
        {
            Id = productId,
            Name = "Test New Name",
            Description = "Test New Description",
            Price = 10
        };

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(oldProduct);

        var result = await _controller.Update(productId, productUpdateDto);

        Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equivalent(expectedResult, ((OkObjectResult)result.Result).Value);
    }

    [Fact]
    public async Task Delete_WhenNotFound_ShouldReturnNotFound()
    {
        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()));

        var result = await _controller.Delete(Guid.NewGuid());

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Delete_WhenSuccessful_ShouldReturnNoContent()
    {
        var productId = Guid.NewGuid();
        var product = new Product { Id = productId, Name = "Test Name", Description = "Test Description", Price = 5 };

        _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);
        _mockProductRepository.Setup(x => x.DeleteAsync(It.IsAny<Product>()));

        var result = await _controller.Delete(productId);

        Assert.IsType<NoContentResult>(result);
    }
}

