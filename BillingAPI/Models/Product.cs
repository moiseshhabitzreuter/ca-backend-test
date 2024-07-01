using System.ComponentModel.DataAnnotations;

namespace BillingAPI.Models
{
    public class Product : BaseModel
    {
        public Product(string id, string productName)
        {
            Id = id;
            ProductName = productName;
            IsDeleted = false;
        }

        [Required]
        public string ProductName { get; set; }
    }
}
