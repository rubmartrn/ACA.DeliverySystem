﻿using ACA.DeliverySystem.UI.Coneverters;
using ACA.DeliverySystem.UI.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACA.DeliverySystem.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient Client)
        {
            _client = Client;
        }

        public async Task<OperationResult> Create(UserAddModel model)
        {
            var response = await _client.PostAsJsonAsync("User", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to create the user."
                };
            }
        }

        public async Task<OperationResult> Update(int id, UserUpdateModel model)
        {
            var response = await _client.PutAsJsonAsync($"User?id={id}", model);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to update the user."
                };
            }
        }


        public async Task<OperationResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"User?id={id}");

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {
                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to delete the user."
                };
            }
        }


        public async Task<IEnumerable<OrderViewModel>> GetUserOrders(int userId)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(),
                               new CustomDateTimeConverter("yyyy-MM-dd") }
                };


                return await _client.GetFromJsonAsync<List<OrderViewModel>>($"User/{userId}/orders", options);

            }
            catch (HttpRequestException m)
            {
                Console.WriteLine(m.Message);
                return Enumerable.Empty<OrderViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<OrderViewModel>();

            }



        }



        public async Task<OperationResult> AddOrderInUser(int userId, OrderAddModel model)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"User/{userId}/orders", model);

                if (response.IsSuccessStatusCode)
                {
                    return OperationResult.Ok();
                }
                else
                {
                    return new OperationResult
                    {
                        Success = false,
                        ErrorMessage = "Failed to add order."
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return OperationResult.Fail("Fail");
            }
            catch (Exception m)
            {

                Console.WriteLine(m.Message);
                return OperationResult.Fail("Fail");
            }

        }

        public async Task<OperationResult<UserViewModel>> SignIn(string email)
        {
            var response = await _client.GetAsync($"User/by-email/{email}");

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadFromJsonAsync<UserViewModel>();
                return OperationResult<UserViewModel>.Ok(user);
            }
            else
            {
                return OperationResult<UserViewModel>.Fail("User not found");
            }
        }

        public async Task<UserViewModel> GetUserById(int id)
        {

            return await _client.GetFromJsonAsync<UserViewModel>($"User/{id}");

        }

    }
}
