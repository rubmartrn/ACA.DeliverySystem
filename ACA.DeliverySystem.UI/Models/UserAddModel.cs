using System.ComponentModel.DataAnnotations;

namespace ACA.DeliverySystem.UI.Models
{
    public class UserAddModel
    {

        public string? Name { get; set; }

        public string? SurName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string Password { get; set; } = null!;

    }
}
