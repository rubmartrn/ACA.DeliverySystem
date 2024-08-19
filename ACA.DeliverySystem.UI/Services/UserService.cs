using ACA.DeliverySystem.UI.Models;
using System.Net.Http.Json;

namespace ACA.DeliverySystem.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient Client)
        {
            _client = Client;
        }

        public async Task Create(UserAddModel model)
        {
            var response = _client.PostAsJsonAsync<User>
        }


    }
}
