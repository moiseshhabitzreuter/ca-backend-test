using BillingAPI.DTO;
using BillingAPI.Models;
using MongoDB.Bson;

namespace BillingAPI.Adapters.BillingAdapter
{
    public class BillingAdapter
    {
        public BillingAdapterResponse CreateBillingsFromDTO(BillingDTO billingDto)
        {
            var billingLinesToAdd = new List<BillingLine>();

            var billingToAdd = new Billing(ObjectId.GenerateNewId().ToString(), billingDto.InvoiceNumber);

            billingToAdd.Customer = billingDto.Customer;
            billingToAdd.Date = billingDto.Date;
            billingToAdd.DueDate = billingDto.DueDate;
            billingToAdd.TotalAmount = billingDto.TotalAmount;
            billingToAdd.Currency = billingDto.Currency;

            if (billingDto.Lines is not null)
            {
                foreach (var billingLine in billingDto.Lines)
                {
                    var billingLineToAdd = new BillingLine(ObjectId.GenerateNewId().ToString(), billingLine.ProductId, billingToAdd.Id);

                    billingLineToAdd.Description = billingLine.Description;
                    billingLineToAdd.Quantity = billingLine.Quantity;
                    billingLineToAdd.UnitPrice = billingLine.UnitPrice;
                    billingLineToAdd.Subtotal = billingLine.Subtotal;

                    billingLinesToAdd.Add(billingLineToAdd);
                }
            };
            return new BillingAdapterResponse(billingToAdd, billingLinesToAdd);
        }
    }
}
