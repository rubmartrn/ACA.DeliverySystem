namespace ACA.DeliverySystem.Business.SignInResponse
{
    public class SignInResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int? UserId { get; set; }
    }
}
