using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.UI.Models;
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


        //Read
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Item>>("Item");
        }

        public async Task<Item> GetbyId(int id)
        {
            return await _client.GetFromJsonAsync<Item>($"Item/{id}");
        }
     
        //Create
        public async Task<OperationResult> Add(ItemAddModel model)
        {
            var response = await _client.PostAsJsonAsync("Item", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }

            return OperationResult.Fail(response.ReasonPhrase!);
        }

        //Delete

        public async Task Delete(int id)
        {
            await _client.DeleteAsync($"Item/{id}");
        }

        //Update

        public async  Task<OperationResult> Update(int id, ItemUpdateModel model)
        {
            var response = await _client.PutAsJsonAsync($"Item/{id}", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }

            return OperationResult.Fail(response.ReasonPhrase!);

        }

    }
}
