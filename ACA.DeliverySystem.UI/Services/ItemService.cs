using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Pages;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

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
        /*public async Task<List<ItemViewModel>> GetAll()
        {
            var response = await _client.GetAsync("http://localhost:5000/api/Item");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to retrieve items: {response.StatusCode}, Content: {content}");
            }
        }*/




        public async Task<IEnumerable<ItemViewModel>> GetAll()
        {
            try
            {
                return await _client.GetFromJsonAsync<List<ItemViewModel>>("Item");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching items: {ex.Message}");
                return new List<ItemViewModel>();
            }
        }




        public async Task<ItemViewModel> GetbyId(int id)
        {
            var respons = await _client.GetFromJsonAsync<ItemViewModel>($"Item/{id}");

            return respons;
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

        public async Task<OperationResult> Update(int id, ItemUpdateModel model)
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
