using System.ComponentModel.DataAnnotations;

namespace ACA.DeliverySystem.Business.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string? Name { get; set; }

        public string? SureName { get; set; }


        [EmailAddress]
        public string? Email { get; set; }
    }
}
