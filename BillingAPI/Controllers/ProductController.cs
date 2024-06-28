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
            var product = _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            _context.Products.InsertOne(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(string id, [FromBody] Product productIn)
        {
            var product = _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            var filter = Builders<Product>.Filter.Eq(c => c.Id, id);
            var update = Builders<Product>.Update
                .Set(c => c.ProductName, productIn.ProductName);

            _context.Products.UpdateOne(filter, update);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(string id)
        {
            var productToDelete = _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefault();

            if (productToDelete == null)
            {
                return NotFound();
            }

            var filter = Builders<Product>.Filter.Eq(c => c.Id, id);
            var update = Builders<Product>.Update
                .Set(c => c.IsDeleted, true);

            _context.Products.UpdateOne(filter, update);

            return NoContent();
        }
    }
}