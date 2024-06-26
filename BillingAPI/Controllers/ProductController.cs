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

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
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

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateProduct(string id, [FromBody] Product productIn)
        {
            var product = _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            //Trocar ReplaceOne por Update
            _context.Products.ReplaceOne(product => product.Id == id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteProduct(string id)
        {
            var productToDelete = _context.Products.Find<Product>(product => product.Id == id && !product.IsDeleted).FirstOrDefault();

            if (productToDelete == null)
            {
                return NotFound();
            }
            //Trocar ReplaceOne por Update
            productToDelete.IsDeleted = true;
            _context.Products.ReplaceOne(product => product.Id == id, productToDelete);

            return NoContent();
        }
    }
}