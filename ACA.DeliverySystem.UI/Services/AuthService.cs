using ACA.DeliverySystem.UI.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

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

    public async Task SignOutAsync()
    {
        await _localStorage.RemoveItemAsync("authToken");
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

