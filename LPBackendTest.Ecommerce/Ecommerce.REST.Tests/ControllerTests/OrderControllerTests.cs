using Ecommerce.REST.Constants;
using Ecommerce.REST.Controllers;
using Ecommerce.REST.Models;
using Ecommerce.REST.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace Ecommerce.REST.Tests.ControllerTests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Get_WithValidOrderId_Returns_OrderAnd200()
        {
            //Arrange
            long orderId = 1001;
            var responseOrder = new Order()
            {
                Id = orderId
            };
            IOrderService _mockOrderService = Substitute.For<IOrderService>();
            _mockOrderService.Get(orderId).Returns(responseOrder);

            IValidationService _mockValidationService = Substitute.For<IValidationService>();

            //Act
            OrderController orderController = new OrderController(_mockValidationService, _mockOrderService);
            var actionResult = orderController.Get(orderId) as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(responseOrder, actionResult.Value);
            _mockOrderService.Received(1).Get(orderId);
        }

        [Fact]
        public void Get_WithInValidOrderId_Returns_404WithMessage()
        {
            //Arrange
            long orderId = 1001;
            Order responseOrder = null;
            IOrderService _mockOrderService = Substitute.For<IOrderService>();
            _mockOrderService.Get(orderId).Returns(responseOrder);

            IValidationService _mockValidationService = Substitute.For<IValidationService>();

            //Act
            OrderController orderController = new OrderController(_mockValidationService, _mockOrderService);
            var actionResult = orderController.Get(orderId) as NotFoundObjectResult;

            //Assert
            Assert.Equal(404, actionResult.StatusCode);
            Assert.Equal("Order not found with id :1001", actionResult.Value);
            _mockOrderService.Received(1).Get(orderId);
        }

        [Fact]
        public void GetAll_Returns_Orders()
        {
            //Arrange
            List<Order> Orders = new List<Order>() {
                new Order() {Id=1001},
                new Order() {Id=1002},
                new Order() {Id=1003},
            };
            IOrderService _mockOrderService = Substitute.For<IOrderService>();
            _mockOrderService.GetAll().Returns(Orders);

            IValidationService _mockValidationService = Substitute.For<IValidationService>();

            //Act
            OrderController orderController = new OrderController(_mockValidationService, _mockOrderService);
            var actionResult = orderController.GetAll() as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            Assert.Equal(Orders, actionResult.Value);
            _mockOrderService.Received(1).GetAll();
        }

        [Fact]
        public void PlaceOrder_Returns_BadRequest()
        {
            //Arrange
            var order = new Order() { };
            ValidationResult validationResult = new ValidationResult()
            {
                ErrorMessage = string.Format(APIConstants.InvalidProductErrorMessage, 100),
                ErrorCode = APIConstants.InvalidProductErrorCode
            };
            List<ValidationResult> lstValidationResult = new List<ValidationResult>() { validationResult };

            IOrderService _mockOrderService = Substitute.For<IOrderService>();

            IValidationService _mockValidationService = Substitute.For<IValidationService>();
            _mockValidationService.OrderValidation(order).Returns(lstValidationResult);

            //Act
            OrderController orderController = new OrderController(_mockValidationService, _mockOrderService);
            var actionResult = orderController.PlaceOrder(order) as BadRequestObjectResult;

            //Assert
            Assert.Equal(400, actionResult.StatusCode);
            Assert.Equal(lstValidationResult, actionResult.Value);
            _mockValidationService.Received(1).OrderValidation(order);
        }

        [Fact]
        public void PlaceOrder_Returns_Ok()
        {
            //Arrange
            var order = new Order() { };
            List<ValidationResult> validationResults = null;
            IOrderService _mockOrderService = Substitute.For<IOrderService>();
            IValidationService _mockValidationService = Substitute.For<IValidationService>();
            _mockValidationService.OrderValidation(order).Returns(validationResults);

            //Act
            OrderController orderController = new OrderController(_mockValidationService, _mockOrderService);
            var actionResult = orderController.PlaceOrder(order) as OkObjectResult;

            //Assert
            Assert.Equal(200, actionResult.StatusCode);
            _mockValidationService.Received(1).OrderValidation(order);
            _mockOrderService.Received(1).PlaceOrder(order);
        }
    }
}
