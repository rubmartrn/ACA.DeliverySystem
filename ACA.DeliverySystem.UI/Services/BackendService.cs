namespace ACA.DeliverySystem.UI.Services
{
    public class BackendService
    {
        private readonly HttpClient _client;

        public BackendService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> TestConnection()
        {
            var response = await _client.GetAsync("api/test");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

    }
}
