using Ecommerce.REST.Controllers;
using Ecommerce.REST.Models;
using Ecommerce.REST.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Ecommerce.REST.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Get_WithValidProductId_Returns_ProductAnd200()
        {
            //Arrange
            long productId = 1001;
            var responseProduct = new Product()
            {
                Id = productId,
                Category = "Test Category",
                Description = "Test Description",
                Name = "TestName",
                Price = 1.5,
                Rating = 2
            };
            IProductService _mockProductService = Substitute.For<IProductService>();
            _mockProductService.Get(productId).Returns(responseProduct);

            //Act
            ProductController productController = new ProductController(_mockProductService);
            var actionResult = productController.Get(productId) as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(responseProduct, actionResult.Value);
            _mockProductService.Received(1).Get(productId);
        }

        [Fact]
        public void Get_WithInValidProductId_Returns_404WithMessage()
        {
            //Arrange
            long productId = 1001;
            Product product = null;
            IProductService _mockProductService = Substitute.For<IProductService>();
            _mockProductService.Get(productId).Returns(product);

            //Act
            ProductController productController = new ProductController(_mockProductService);
            var actionResult = productController.Get(productId) as NotFoundObjectResult;

            //Assert
            Assert.Equal(404, actionResult.StatusCode);
            Assert.Equal("Product not found with id :1001", actionResult.Value);
            _mockProductService.Received(1).Get(productId);
        }

        [Fact]
        public void GetAll_Returns_Products()
        {
            //Arrange
            List<Product> products = new List<Product>() {
                new Product() {Id=1001},
                new Product() {Id=1002},
                new Product() {Id=1003},
            };
            IProductService _mockProductService = Substitute.For<IProductService>();
            _mockProductService.GetAll().Returns(products);

            //Act
            ProductController productController = new ProductController(_mockProductService);
            var actionResult = productController.GetAll() as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(products, actionResult.Value);
            _mockProductService.Received(1).GetAll();
        }

        [Fact]
        public void Search_Returns_Products()
        {
            //Arrange
            List<Product> products = new List<Product>() {
                new Product() {Id=1001, Category="pen"},
                new Product() {Id=1002, Category="pen"},
                new Product() {Id=1003, Name="Gel Pen"},
            };
            IProductService _mockProductService = Substitute.For<IProductService>();
            _mockProductService.Search("pen").Returns(products);

            //Act
            ProductController productController = new ProductController(_mockProductService);
            var actionResult = productController.Search("pen") as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(products, actionResult.Value);
            _mockProductService.Received(1).Search("pen");
        }
    }
}
