using BillingAPI.Data;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public ProductController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(string id)
        {
            var product = _context.Products.FirstOrDefault<Product>(product => product.Id == id && !product.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] Product productIn)
        {
            var product = _context.Products.FirstOrDefault<Product>(product => product.Id == id && !product.IsDeleted);

            if (product == null)
            {
                return NotFound();
            }

            product.ProductName = productIn.ProductName;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var productToDelete = _context.Products.FirstOrDefault<Product>(product => product.Id == id && !product.IsDeleted);

            if (productToDelete == null)
            {
                return NotFound();
            }

            productToDelete.IsDeleted = true;

            _context.SaveChanges();
            return NoContent();
        }
    }
}