using BillingAPI.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BillingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBillings()
        {
            try
            {
                var response = await _billingService.CreateBillingsAsync();
                return Ok(response);
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
    }
}
