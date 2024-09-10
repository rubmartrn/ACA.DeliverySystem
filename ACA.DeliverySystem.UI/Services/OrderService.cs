

using ACA.DeliverySystem.UI.Coneverters;
using ACA.DeliverySystem.UI.Models;
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

        public async Task<IEnumerable<OrderViewModel>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<OrderViewModel>>("Order");
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

                    return order!;

                }
                return new OrderViewModel();

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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
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
                var errorMessage = await response.Content.ReadAsStringAsync();
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = errorMessage
                };
            }
        }

    }
}
