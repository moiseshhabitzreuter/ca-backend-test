using BillingAPI.Adapters.BillingAdapter;
using BillingAPI.Common;
using BillingAPI.Data;
using BillingAPI.DTO;
using BillingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly HttpClient _httpClient;
        private BillingAdapter _billingAdapter = new BillingAdapter();
        private Response _response = new Response();

        public BillingController(MongoDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public IActionResult CreateBillings()
        {
            try
            {
                var apiResponse = _httpClient.GetStringAsync("apiurl").Result;

                var billingDTOs = JsonConvert.DeserializeObject<List<BillingDTO>>(apiResponse);

                foreach (var billing in billingDTOs)
                {
                    InsertBilling(billing);
                }

                return Ok(_response);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Service Unavailable: Unable to reach the external API.");
            }
            catch (JsonException ex)
            {
                return StatusCode(500, "Internal Server Error: Invalid JSON response from the external API.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        private void InsertBilling(BillingDTO billingDTO)
        {
            var billingsAreValid = ValidateBillings(billingDTO);
            if (billingsAreValid)
            {
                var adapterResponse = _billingAdapter.CreateBillingsFromDTO(billingDTO);
                _context.Billings.Add(adapterResponse.BillingToCreate);
                _context.BillingLines.AddRange(adapterResponse.BillingLinesToCreate);
                _context.SaveChanges();
                _response.AddBillingInsertedSuccessfully($"{billingDTO.InvoiceNumber}");
            }
        }

        private bool ValidateBillings(BillingDTO billingDTO)
        {
            var shouldProcceed = true;
            if (billingDTO.Customer is null)
            {
                _response.AddError($"Customer is Empty on Billing:{billingDTO.InvoiceNumber}");
                shouldProcceed = false;
            }
            else
            {
                var customer = _context.Customers.FirstOrDefault<Customer>(customer => customer.Id == billingDTO.Customer.Id && !customer.IsDeleted);

                if (customer == null)
                {
                    _response.AddError($"Customer not found on Billing:{billingDTO.InvoiceNumber}");
                    shouldProcceed = false;
                }
            }

            if (billingDTO.Lines is not null)
            {
                foreach (var billingLine in billingDTO.Lines)
                {
                    var product = _context.Products.FirstOrDefault<Product>(product => product.Id == billingLine.ProductId && !product.IsDeleted);

                    if (product == null)
                    {
                        _response.AddError($"Product not found on Billing:{billingDTO.InvoiceNumber}");
                        shouldProcceed = false;
                    }
                }
            }
            return shouldProcceed;
        }
    }
}
