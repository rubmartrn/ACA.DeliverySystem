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

        public async Task<OperationResult> Delete(int id, CancellationToken token)
        {
            var response = await _client.DeleteAsync($"Item?id={id}", token);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OperationResult>();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to delete the item."
                };
            }
        }


        //Update

        public async  Task<OperationResult> Update(int id, ItemUpdateModel model)
        {
            var response = await _client.PutAsJsonAsync($"Item?id={id}", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }

            return OperationResult.Fail(response.ReasonPhrase!);

        }

    }
}
