using ACA.DeliverySystem.Business.Models;

namespace ACA.DeliverySystem.Business.Services
{
    public interface ITokenService
    {
        string GenerateToken(ResponseForSignIn user);
    }
}