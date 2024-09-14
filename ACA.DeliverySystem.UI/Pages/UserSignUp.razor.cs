using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignUpBase : ComponentBase
    {
        protected UserAddModel? _userModel { get; set; }
        protected string? _errorMessage;
        protected string? _passwordValidationMessage;
        protected string? _passwordConfirmation;
        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override void OnInitialized()
        {
            _userModel = new UserAddModel();
            _errorMessage = string.Empty;
            _passwordValidationMessage = string.Empty;
            _passwordConfirmation = string.Empty;
        }

        protected async Task HandleSubmit()
        {
            // Check if the password meets requirements
            if (!ValidatePassword(_userModel!.Password))
            {
                Snackbar.Add("Password does not meet the requirements.", Severity.Error);
                return;
            }

            // Check if the passwords match
            if (_userModel!.Password != _passwordConfirmation)
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

        protected bool ValidatePassword(string password)
        {
            var errors = new List<string>();

            if (password.Length < 8) errors.Add("Password must be at least 8 characters long.");
            if (!password.Any(char.IsUpper)) errors.Add("Password must contain at least one uppercase letter.");
            if (!password.Any(char.IsLower)) errors.Add("Password must contain at least one lowercase letter.");
            if (!password.Any(char.IsDigit)) errors.Add("Password must contain at least one number.");
            if (!password.Any(c => "!@#$%^&*()_+[]{}|;:',.<>?/".Contains(c))) errors.Add("Password must contain at least one special character.");
            if (password.Any(char.IsWhiteSpace)) errors.Add("Password must not contain spaces.");

            _passwordValidationMessage = string.Join("<br>", errors);

            return !errors.Any();
        }
    }
}