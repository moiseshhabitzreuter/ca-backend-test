using BillingAPI.Data;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public CustomerController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomer(string id)
        {
            var customer = _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            _context.Customers.InsertOne(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, [FromBody] Customer customerIn)
        {
            var customer = _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            var update = Builders<Customer>.Update
                .Set(c => c.Name, customerIn.Name)
                .Set(c => c.Email, customerIn.Email)
                .Set(c => c.Address, customerIn.Address);

            _context.Customers.UpdateOne(filter, update);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customerToDelete = _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefault();

            if (customerToDelete == null)
            {
                return NotFound();
            }

            var filter = Builders<Customer>.Filter.Eq(c => c.Id, id);
            var update = Builders<Customer>.Update
                .Set(c => c.IsDeleted, true);

            _context.Customers.UpdateOne(filter, update);

            return NoContent();
        }
    }
}
