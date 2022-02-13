using Ecommerce.REST.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public ActionResult Get(long id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound($"Product not found with id :{id}");
            }

            return Ok(product);
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("Search/{searchText}")]
        public ActionResult Search(string searchText)
        {
            return Ok(_productService.Search(searchText));
        }
    }
}
