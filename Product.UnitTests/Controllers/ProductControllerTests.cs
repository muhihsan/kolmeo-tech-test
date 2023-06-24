using System;
using API.Model;
using API.Repositories;
using Controllers;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests.Controllers
{
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
        public void GetAll_WhenSuccessful_ShouldReturnAllProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = new Guid(), Name = "Test Name", Description = "Test Description", Price = 5 }
            };

            _mockProductRepository.Setup(x => x.GetAll()).Returns(products);

            var results = _controller.GetAll();

            Assert.Equal(products, results);
        }
    }
}

