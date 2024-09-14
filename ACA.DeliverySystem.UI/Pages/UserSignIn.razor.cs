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
            var result = await UserService.SignIn(_loginModel!);
            if (result.Success)
            {
                var userId = result.Data!.Id!;
                NavigationManager.NavigateTo($"/User/{userId}");
            }
            else
            {
                _errorMessage = result.ErrorMessage!;
            }
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
