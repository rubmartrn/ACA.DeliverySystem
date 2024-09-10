

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
                // Ապահովում ենք, որ սերվերի պատասխանը ստացվում է ճիշտ կերպ
                var response = await _client.GetAsync($"Order/{id}", token);

                if (response.IsSuccessStatusCode)
                {
                    // Ստանում ենք JSON պատասխանը որպես տեքստ
                    var json = await response.Content.ReadAsStringAsync();

                    // Deserialize ամբողջական OrderViewModel-ը
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

                    if (order == null)
                    {
                        Console.WriteLine("Order is null.");
                        return new OrderViewModel(); // Եթե order-ը null է, վերադարձնում ենք դատարկ մոդել
                    }

                    // Տպում ենք ստացված Order-ի տվյալները՝ debugging նպատակով
                    Console.WriteLine($"Order ID: {order.Id}, Name: {order.Name}");

                    // Եթե order-ը ունի items, տպում ենք դրանց տվյալները
                    if (order.Items != null)
                    {
                        foreach (var item in order.Items)
                        {
                            Console.WriteLine($"Order Item: {item.ItemName}, Quantity: {item.Quantity}");

                            if (item.Item != null)
                            {
                                Console.WriteLine($"Item: {item.Item.Name}, Price: {item.Item.Price}");
                            }
                        }
                    }

                    return order;
                }

                // Եթե պատասխանը չի հաջողվել, վերադարձնում ենք դատարկ մոդել
                return new OrderViewModel();
            }
            catch (HttpRequestException m)
            {
                Console.WriteLine($"HTTP request error: {m.Message}");
                return new OrderViewModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
