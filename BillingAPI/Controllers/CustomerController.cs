using BillingAPI.Data;
using BillingAPI.Interfaces.Services;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(string id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            await _customerService.CreateCustomerAsync(customer);
            return CreatedAtRoute("GetCustomer", new { id = customer.Id.ToString() }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] Customer customerIn)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.UpdateCustomerAsync(id, customerIn);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customerToDelete = await _customerService.GetCustomerByIdAsync(id);

            if (customerToDelete == null)
            {
                return NotFound();
            }

            await _customerService.DeleteCustomerAsync(id);

            return NoContent();
        }
    }
}
