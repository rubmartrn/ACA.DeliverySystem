using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignUpBase : ComponentBase
    {
        protected UserAddModel? _userModel { get; set; }
        protected string _errorMessage;
        protected string _passwordConfirmation; // New field for confirming password
        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override void OnInitialized()
        {
            _userModel = new UserAddModel();
            _errorMessage = string.Empty;
            _passwordConfirmation = string.Empty;
        }

        protected async Task HandleSubmit()
        {
            // Check if the passwords match
            if (_userModel.Password != _passwordConfirmation)
            {
                _errorMessage = "Passwords do not match.";
                Snackbar.Add(_errorMessage, Severity.Error);
                return;
            }

            var result = await UserService.Create(_userModel);
            if (result.Success)
            {
                NavigationManager.NavigateTo("/signin");
            }
            else
            {
                Snackbar.Add(result.ErrorMessage, Severity.Error);
                _errorMessage = result.ErrorMessage;
            }
        }
    }
}