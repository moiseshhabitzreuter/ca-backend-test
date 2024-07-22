using BillingAPI.Adapters.BillingAdapter;
using BillingAPI.Common;
using BillingAPI.DTO;
using BillingAPI.Interfaces.Data;
using BillingAPI.Interfaces.Services;
using Newtonsoft.Json;

namespace BillingAPI.Services
{
    public class BillingService : IBillingService
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IBillingLinesRepository _billingLinesRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly HttpClient _httpClient;
        private BillingAdapter _billingAdapter = new BillingAdapter();
        private Response _response = new Response();

        public BillingService(IBillingRepository billingRepository, IBillingLinesRepository billingLinesRepository, ICustomerRepository customerRepository, IProductRepository productRepository, HttpClient httpClient)
        {
            _billingRepository = billingRepository;
            _billingLinesRepository = billingLinesRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _httpClient = httpClient;
        }

        public async Task<Response> CreateBillingsAsync()
        {
            var apiResponse = await _httpClient.GetStringAsync("apiurl");

            var billingDTOs = JsonConvert.DeserializeObject<List<BillingDTO>>(apiResponse);

            foreach (var billing in billingDTOs)
            {
                await InsertBilling(billing);
            }

            return _response;
        }

        private async Task InsertBilling(BillingDTO billingDTO)
        {
            var billingsAreValid = await ValidateBillings(billingDTO);
            if (billingsAreValid)
            {
                var adapterResponse = _billingAdapter.CreateBillingsFromDTO(billingDTO);

                await _billingRepository.InsertBillingAsync(adapterResponse.BillingToCreate);
                await _billingLinesRepository.InsertBillingLinesAsync(adapterResponse.BillingLinesToCreate);

                _response.AddBillingInsertedSuccessfully($"{billingDTO.InvoiceNumber}");
            }
        }

        private async Task<bool> ValidateBillings(BillingDTO billingDTO)
        {
            var shouldProcceed = true;
            if (billingDTO.Customer is null)
            {
                _response.AddError($"Customer is Empty on Billing:{billingDTO.InvoiceNumber}");
                shouldProcceed = false;
            }
            else
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(billingDTO.Customer.Id);

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
                    var product = await _productRepository.GetProductByIdAsync(billingLine.ProductId);

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
