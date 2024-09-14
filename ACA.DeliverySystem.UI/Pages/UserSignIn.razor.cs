using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignInBase : ComponentBase
    {
        protected SignInRequestModel? _loginModel { get; set; }
        protected string? _errorMessage;

        // Variables to control password input type and icon
        protected string _passwordInputType = "password";
        protected string _passwordIcon = Icons.Material.Filled.VisibilityOff;

        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override void OnInitialized()
        {
            _loginModel = new SignInRequestModel();
            _errorMessage = string.Empty;
        }

        protected async Task HandleSubmit()
        {
            var result = await AuthService.SignInAsync(_loginModel!);
            if (result.Success)
            {
                var token = result.Data!.Token; // assuming `Token` is part of the response
                await AuthService.StoreTokenAsync(token!); // Store the token

                NavigationManager.NavigateTo($"/User/{result.Data!.Id}");
            }
            else
            {
                _errorMessage = result.ErrorMessage;
                Snackbar.Add(_errorMessage, Severity.Error);
            }

            //var result = await UserService.SignIn(_loginModel!);
            //if (result.Success)
            //{
            //    NavigationManager.NavigateTo($"/User/{result.Data!.Id}");
            //}
            //else
            //{
            //    Snackbar.Add($"{_errorMessage}", Severity.Error);
            //}

        }

        // Toggle password visibility
        protected void TogglePasswordVisibility()
        {
            if (_passwordInputType == "password")
            {
                _passwordInputType = "text";
                _passwordIcon = Icons.Material.Filled.Visibility;
            }
            else
            {
                _passwordInputType = "password";
                _passwordIcon = Icons.Material.Filled.VisibilityOff;
            }
        }
    }
}
