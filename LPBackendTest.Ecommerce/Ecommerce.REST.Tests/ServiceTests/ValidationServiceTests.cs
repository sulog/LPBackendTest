using Ecommerce.REST.Constants;
using Ecommerce.REST.Models;
using Ecommerce.REST.Repositories.Contracts;
using Ecommerce.REST.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ecommerce.REST.Tests.ServiceTests
{
    public class ValidationServiceTests
    {
        [Fact]
        public void OrderValidation_Returns_InvalidProduct()
        {
            //Arrange
            long productId = 1001;
            Order order = new Order()
            {
                Products = new List<Product>(){
                    new Product()
                    {
                        Id=productId
                    }
                }
            };
            IProductRepository _mockProductRepository = Substitute.For<IProductRepository>();
            _mockProductRepository.IsProductExist(productId).Returns(false);

            //Act
            ValidationService validationService = new ValidationService(_mockProductRepository);
            var result = validationService.OrderValidation(order);

            //Assert
            Assert.Single(result);
            Assert.Equal(result[0].ErrorCode, APIConstants.InvalidProductErrorCode);
            Assert.Equal(result[0].ErrorMessage, string.Format(APIConstants.InvalidProductErrorMessage, productId));
        }

        [Fact]
        public void OrderValidation_Returns_ProductUnavailable()
        {
            //Arrange
            long productId = 1001;
            Order order = new Order()
            {
                Products = new List<Product>(){
                    new Product()
                    {
                        Id=productId
                    }
                }
            };
            IProductRepository _mockProductRepository = Substitute.For<IProductRepository>();
            _mockProductRepository.IsProductExist(productId).Returns(true);
            _mockProductRepository.IsProductAvailable(productId).Returns(false);

            //Act
            ValidationService validationService = new ValidationService(_mockProductRepository);
            var result = validationService.OrderValidation(order);

            //Assert
            Assert.Single(result);
            Assert.Equal(result[0].ErrorCode, APIConstants.ProductNotAvailableErrorCode);
            Assert.Equal(result[0].ErrorMessage, string.Format(APIConstants.ProductNotAvailableErrorMessage, productId));
        }

        [Fact]
        public void OrderValidation_Returns_EmptyValidationResult()
        {
            //Arrange
            long productId = 1001;
            Order order = new Order()
            {
                Products = new List<Product>(){
                    new Product()
                    {
                        Id=productId
                    }
                }
            };
            IProductRepository _mockProductRepository = Substitute.For<IProductRepository>();
            _mockProductRepository.IsProductExist(productId).Returns(true);
            _mockProductRepository.IsProductAvailable(productId).Returns(true);

            //Act
            ValidationService validationService = new ValidationService(_mockProductRepository);
            var result = validationService.OrderValidation(order);

            //Assert
            Assert.Empty(result);
        }
    }
}
