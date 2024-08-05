using System.ComponentModel.DataAnnotations;

namespace ACA.DeliverySystem.Data.Models
{
    public class User
    {

        public int Id { get; set; }

        public int? OrderId { get; set; }

        public string Name { get; set; }

        public string SureName { get; set; }

        public Order Order { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; }
    }
}
