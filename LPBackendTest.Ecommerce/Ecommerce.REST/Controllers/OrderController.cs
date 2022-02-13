using Ecommerce.REST.Models;
using Ecommerce.REST.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IValidationService _validationService;
        private IOrderService _orderService;
        public OrderController(IValidationService validationService, IOrderService orderService)
        {
            _validationService = validationService;
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult PlaceOrder(Order order)
        {
            var validationResult = _validationService.OrderValidation(order);

            if (validationResult != null && validationResult.Count > 0)
            {
                return BadRequest(validationResult);
            }
            _orderService.PlaceOrder(order);
            return Ok(order);
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var order = _orderService.Get(id);

            if (order == null)
            {
                return NotFound($"Order not found with id :{id}");
            }

            return Ok(order);
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_orderService.GetAll());
        }
    }
}
