using System.ComponentModel.DataAnnotations;

namespace ACA.DeliverySystem.Business.Models
{
    public class UserAddModel
    {
        public string? Name { get; set; }

        public string? SureName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
    }
}
