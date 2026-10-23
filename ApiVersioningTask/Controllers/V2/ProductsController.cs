using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioningTask.Controllers.V2
{
    [Route("api/v{version:apiVersion}/products")]
    [ApiVersion(2.0)]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var product = new
            {
                Id = id,
                Name = "Smart watch",
                Price = 300,
                categoryName = "Electronics"
            };
            return Ok(product);
        }
    }
}