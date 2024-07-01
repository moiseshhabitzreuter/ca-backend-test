using System.ComponentModel.DataAnnotations;

namespace BillingAPI.Models
{
    public class BaseModel
    {

        [Key]
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
