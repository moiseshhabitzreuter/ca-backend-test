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
            var customer = _context.Customers.FirstOrDefault<Customer>(customer => customer.Id == id && !customer.IsDeleted);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(string id, [FromBody] Customer customerIn)
        {
            var customer = _context.Customers.FirstOrDefault<Customer>(customer => customer.Id == id && !customer.IsDeleted);

            //Add validation to empty list
            if (customer is null)
            {
                return NotFound();
            }

            customer.Address = customerIn.Address;
            customer.Email = customerIn.Email;
            customer.Name = customerIn.Name;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(string id)
        {
            var customerToDelete = _context.Customers.FirstOrDefault<Customer>(customer => customer.Id == id && !customer.IsDeleted);

            if (customerToDelete == null)
            {
                return NotFound();
            }

            customerToDelete.IsDeleted = true;

            _context.SaveChanges();
            return NoContent();
        }
    }
}
