using System.ComponentModel.DataAnnotations;

namespace BillingAPI.Models
{
    public class Customer : BaseModel
    {
        public Customer(string id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            IsDeleted = false;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
