﻿

using ACA.DeliverySystem.UI.Coneverters;
using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Pages;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<OrderViewModel> GetById(int id, CancellationToken token)
        {
            try
            {
                var response = await _client.GetAsync($"Order/{id}", token);


                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var order = JsonSerializer.Deserialize<OrderViewModel>(json, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        PropertyNameCaseInsensitive = true,
                        Converters =
                {
                    new JsonStringEnumConverter(),
                    new CustomDateTimeConverter("dd-MM-yyyy")
                }
                    });

                    return order;

                }
                return new OrderViewModel();
                // return await _client.GetFromJsonAsync<OrderViewModel>($"Order/{id}");

            }
            catch (HttpRequestException m)
            {
                Console.WriteLine(m.Message);
                return new OrderViewModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OrderViewModel();

            }
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


        public async Task<OperationResult> CancelOrder(int id, CancellationToken token)
        {
            var response = await _client.PostAsync($"Order/cancelOrder?orderId={id}", null, token);


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
            var response = await _client.DeleteAsync($"Order?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {

                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to delete the order. You can only delete empty orders."
                };
            }
        }


        public async Task<OperationResult> RemoveItemFromOrder(int orderId, int itemId)
        {
            var response = await _client.DeleteAsync($"Order/removeItemFromOrder?orderId={orderId}&itemId={itemId}");

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
            var response = await _client.GetAsync($"Order/payment?orderId={orderId}&amount={amount}");

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
            var response = await _client.PostAsync($"Order/orderCompleted?orderId={orderId}", null);

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
