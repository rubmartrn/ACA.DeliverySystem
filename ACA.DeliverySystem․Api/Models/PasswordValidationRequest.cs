namespace ACA.DeliverySystem.Api.Models
{
    public class PasswordValidationRequest
    {
        public int Id { get; set; }
        public string Password { get; set; } = null!;
    }
}
