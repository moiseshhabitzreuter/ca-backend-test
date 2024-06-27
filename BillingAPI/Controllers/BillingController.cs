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
            var apiResponse = _httpClient.GetStringAsync("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing").Result;
            var billingDTOs = JsonConvert.DeserializeObject<List<BillingDTO>>(apiResponse);

            foreach (var billing in billingDTOs)
            {
                InsertBilling(billing);
            }

            return Ok(_response);
        }

        private void InsertBilling(BillingDTO billingDTO)
        {
            var billingsAreValid = ValidateBillings(billingDTO);
            if (billingsAreValid)
            {
                var adapterResponse = _billingAdapter.CreateBillingsFromDTO(billingDTO);
                _context.Billings.InsertOne(adapterResponse.BillingToCreate);
                _context.BillingLines.InsertMany(adapterResponse.BillingLinesToCreate);
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
                var customer = _context.Customers.Find<Customer>(customer => customer.Id == billingDTO.Customer.Id && !customer.IsDeleted).FirstOrDefault();

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
                    var product = _context.Products.Find<Product>(product => product.Id == billingLine.ProductId && !product.IsDeleted).FirstOrDefault();

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
