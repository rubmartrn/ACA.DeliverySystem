using ACA.DeliverySystem.UI.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public int userId { get; set; }
    public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<OperationResult<ResponseForSignIn>> SignInAsync(SignInRequestModel model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("User/sign-in", model);


            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseForSignIn>();


                if (result != null && !string.IsNullOrEmpty(result.Token))
                {
                    // Save token in localStorage
                    await _localStorage.SetItemAsync("authToken", result.Token);
                    await _localStorage.SetItemAsync("userId", result.Id);


                    return OperationResult<ResponseForSignIn>.Ok(result);
                }
            }
            return OperationResult<ResponseForSignIn>.Fail();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return OperationResult<ResponseForSignIn>.Fail();

        }

    }
    public bool IsTokenValid(string token)
    {
        var jwtHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

        if (jwtHandler.CanReadToken(token))
        {
            var jwtToken = jwtHandler.ReadJwtToken(token);
            var expirationDate = jwtToken.ValidTo;

            // Ստուգել՝ արդյոք ժամկետը լրացել է
            if (expirationDate > DateTime.UtcNow)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<bool> CheckAuthenticationAsync()
    {
        var token = await GetTokenAsync();

        if (string.IsNullOrEmpty(token) || !IsTokenValid(token))
        {
            return false;
        }

        return true;
    }

    public async Task<int> GetUserIdAsync()
    {
       
        return await _localStorage.GetItemAsync<int>("userId");
    }

    public async Task SignOutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _localStorage.RemoveItemAsync("userId");
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _localStorage.GetItemAsync<string>("authToken");
    }

    public async Task StoreTokenAsync(string token)
    {
        await _localStorage.SetItemAsync("authToken", token);
    }

}

