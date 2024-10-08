﻿using ACA.DeliverySystem.UI.Coneverters;
using ACA.DeliverySystem.UI.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACA.DeliverySystem.UI.Services
{
    public class UserService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient Client, ILocalStorageService localStorage)
        {
            _client = Client;
            _localStorage = localStorage;
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

        // for deleting user, but I don't give a user permission to do that
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


        public async Task<IEnumerable<OrderViewModel>> GetUserOrders(int userId, CancellationToken token)
        {

            var response = await _client.GetAsync($"User/{userId}/orders", token);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<IEnumerable<OrderViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter(),
                            new CustomDateTimeConverter("MM-dd-yyyy")
                    },
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                });
                return orders!;

            }
            return Enumerable.Empty<OrderViewModel>();

        }

        public async Task<OperationResult> AddOrderInUser(int userId, OrderAddModel model)
        {
            try
            {
                var response = await _client.PostAsJsonAsync($"User/addOrder?userId={userId}", model);

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


        //public async Task<OperationResult<ResponseForSignIn>> SignIn(SignInRequestModel model)
        //{
        //    var response = await _client.PostAsJsonAsync("User/sign-in", model);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var resultData = await response.Content.ReadFromJsonAsync<ResponseForSignIn>();

        //        return OperationResult<ResponseForSignIn>.Ok(resultData!);

        //    }
        //    var errorMessage = await response.Content.ReadAsStringAsync();
        //    return new OperationResult<ResponseForSignIn>
        //    {
        //        Success = false,
        //        ErrorMessage = errorMessage
        //    };
        //}


        public async Task<UserViewModel?> GetUserById(int id)
        {

            return await _client.GetFromJsonAsync<UserViewModel>($"User/{id}");

        }

        public async Task<OperationResult> UpdatePasswordAsync(PasswordChangeRequest request)
        {
            var result = await _client.PutAsJsonAsync($"User/changePassword", request);
            if (result.IsSuccessStatusCode)
            {
                return OperationResult.Ok();
            }
            else
            {

                return new OperationResult
                {
                    Success = false,
                    ErrorMessage = "Failed to update password"
                };
            }
        }

    }
}
