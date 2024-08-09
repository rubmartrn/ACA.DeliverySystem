using ACA.DeliverySystem.UI.Pages;
using System.Net.Http.Json;

namespace ACA.DeliverySystem.UI.Services
{
    public class ItemService
    {
        private readonly HttpClient _client;

        public ItemService(HttpClient client)
        {
            _client = client;
        }


        public async Task<IEnumerable<Item>> TestGetAllItems()
        {
            return await _client.GetFromJsonAsync<List<Item>>("Item");
        }


     

    }
}
