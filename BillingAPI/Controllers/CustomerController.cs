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

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
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

        [HttpPut("{id:length(24)}")]
        public IActionResult UpdateCustomer(string id, [FromBody] Customer customerIn)
        {
            var customer = _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            //Trocar ReplaceOne por Update
            _context.Customers.ReplaceOne(customer => customer.Id == id, customerIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customerToDelete = _context.Customers.Find<Customer>(customer => customer.Id == id && !customer.IsDeleted).FirstOrDefault();

            if (customerToDelete == null)
            {
                return NotFound();
            }
            //Trocar ReplaceOne por Update
            customerToDelete.IsDeleted = true;
            _context.Customers.ReplaceOne(customer => customer.Id == id, customerToDelete);

            return NoContent();
        }
    }
}
