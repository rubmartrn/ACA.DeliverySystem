

namespace ACA.DeliverySystem.Business.Models
{
    public class ResponseForSignIn
    {
        public int Id { get; set; }

        public string? PasswordHash { get; set; }

        public string? Token { get; set; }
    }
}
