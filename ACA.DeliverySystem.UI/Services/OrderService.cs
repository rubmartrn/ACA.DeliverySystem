

using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Pages;
using System.Net.Http.Json;
using static MudBlazor.Colors;

namespace ACA.DeliverySystem.UI.Services
{
    public class OrderService
    {

        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Order>>("Order");
        }

        public async Task<Order> GetById(int id)
        {
            return await _client.GetFromJsonAsync<Order>($"Order/{id}");
        }

        public async Task<OperationResult> Create(OrderAddModel model)
        {
            var response = await _client.PostAsJsonAsync("Order", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to create the Order."
                };
            }
        }


        public async Task<OperationResult> AddItemInOrder(int orderId, int itemId)
        {
            var response = await _client.PostAsJsonAsync($"Order/addItemInOrder?orderId={orderId}&itemId={itemId}", new { });

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to add item to the order."
                };
            }
        }


        public async Task<OperationResult> CancelOrder(int id)
        {
            var response = await _client.PostAsJsonAsync($"Order/cancelOrder?orderId={id}", new { });


            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to cancel the order."
                };
            }
        }



        public async Task<OperationResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"Order/{id}");

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to delete the order."
                };
            }
        }


        public async Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId)
        {
            var response = await _client.DeleteAsync($"Order/{orderId}/items/{itemId}");

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
            
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to remove the item from the order."
                };
            }
        }

        public async Task<OperationResult> PayForOrder(int orderId, decimal amount)
        {
            var response = await _client.PostAsJsonAsync($"Order/{orderId}/pay", new { amount });

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to process the payment."
                };
            }
        }



        public async Task<OperationResult> OrderCompleted(int orderId)
        {
            var response = await _client.PostAsync($"Order/{orderId}/complete",null);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to complete the order."
                };
            }
        }

    }
}
