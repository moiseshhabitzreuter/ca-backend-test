using BillingAPI.DTO;
using BillingAPI.Models;
using MongoDB.Bson;

namespace BillingAPI.Adapters.BillingAdapter
{
    public class BillingAdapter
    {
        public BillingAdapterResponse CreateBillingsFromDTO(List<BillingDTO> billingDto)
        {
            var billingsToAdd = new List<Billing>();
            var billingLinesToAdd = new List<BillingLine>();

            foreach (var billing in billingDto)
            {
                var billingToAdd = new Billing(ObjectId.GenerateNewId().ToString(), billing.InvoiceNumber);

                billingToAdd.Customer = billing.Customer;
                billingToAdd.Date = billing.Date;
                billingToAdd.DueDate = billing.DueDate;
                billingToAdd.TotalAmount = billing.TotalAmount;
                billingToAdd.Currency = billing.Currency;

                billingsToAdd.Add(billingToAdd);
                if (billing.Lines is not null)
                {
                    foreach (var billingLine in billing.Lines)
                    {
                        var billingLineToAdd = new BillingLine(ObjectId.GenerateNewId().ToString(), billingLine.ProductId, billingToAdd.Id);

                        billingLineToAdd.Description = billingLine.Description;
                        billingLineToAdd.Quantity = billingLine.Quantity;
                        billingLineToAdd.UnitPrice = billingLine.UnitPrice;
                        billingLineToAdd.Subtotal = billingLine.Subtotal;

                        billingLinesToAdd.Add(billingLineToAdd);
                    }
                };
            }
            return new BillingAdapterResponse(billingsToAdd, billingLinesToAdd);
        }
    }
}
