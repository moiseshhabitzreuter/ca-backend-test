
namespace BillingAPI.Common
{
    public class Response
    {
        public Response()
        {
            BillingsInsertedSuccessfully = new List<string>();
            ErrorMessages = new List<string>();
        }
        public List<string> BillingsInsertedSuccessfully { get; set; }
        public List<string> ErrorMessages { get; set; }

        public void AddBillingInsertedSuccessfully(string billingInvoiceNumber)
        {
            this.BillingsInsertedSuccessfully.Add(billingInvoiceNumber);
        }

        public void AddError(string message)
        {
            this.ErrorMessages.Add(message);
        }
    }
}
