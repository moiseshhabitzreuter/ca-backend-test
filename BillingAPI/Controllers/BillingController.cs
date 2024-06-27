using BillingAPI.Adapters.BillingAdapter;
using BillingAPI.Data;
using BillingAPI.DTO;
using Microsoft.AspNetCore.Mvc;
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

        public BillingController(MongoDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public IActionResult CreateBillings()
        {
            var response = _httpClient.GetStringAsync("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing").Result;
            var apiResponse = JsonConvert.DeserializeObject<List<BillingDTO>>(response);

            var adapterResponse = _billingAdapter.CreateBillingsFromDTO(apiResponse);

            _context.Billings.InsertMany(adapterResponse.BillingsToCreate);
            _context.BillingLines.InsertMany(adapterResponse.BillingLinesToCreate);

            return Ok(adapterResponse);
        }
    }
}
