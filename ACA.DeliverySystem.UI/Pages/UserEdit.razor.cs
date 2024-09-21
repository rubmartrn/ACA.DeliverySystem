using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserEditBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }
        protected string _confirmNewPassword = string.Empty;
        protected string? _passwordValidationMessage;

        // Variables to control password input type and icons
        protected string _currentPasswordInputType = "password";
        protected string _newPasswordInputType = "password";
        protected string _confirmPasswordInputType = "password";
        protected string _currentPasswordIcon = Icons.Material.Filled.VisibilityOff;
        protected string _newPasswordIcon = Icons.Material.Filled.VisibilityOff;
        protected string _confirmPasswordIcon = Icons.Material.Filled.VisibilityOff;

        protected UserUpdateModel? _userModel { get; set; }
        protected PasswordValidationRequest _passwordValidationRequest { get; set; } = new PasswordValidationRequest();
        protected PasswordChangeRequest _passwordChangeRequest { get; set; } = new PasswordChangeRequest();

        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject] protected AuthService AuthService { get; set; } = default!;
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            _userModel = new UserUpdateModel();
            var user = await UserService.GetUserById(userId);
            _userModel.Name = user!.Name;
            _userModel.SurName = user.SurName;
            _passwordChangeRequest.Id = userId;
            _passwordValidationRequest.Id = userId;
        }

        protected async Task HandleEditNameAndSurname()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            await UserService.Update(userId, _userModel!);
            Snackbar.Add("User details updated successfully.", Severity.Success);
            NavigationManager.NavigateTo($"/User/{userId}");
        }

        protected async Task HandleChangePassword()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            // Check if the password meets requirements
            if (!ValidatePassword(_passwordChangeRequest.Password))
            {
                Snackbar.Add("Password does not meet the requirements.", Severity.Error);
                return;
            }
            if (_passwordChangeRequest.Password != _confirmNewPassword)
            {
                _passwordValidationMessage = "Passwords do not match.";
                return;
            }
            // Perform password validation logic...
            var isCurrentPasswordValid = await AuthService.ValidatePassword(_passwordValidationRequest);
            if (!isCurrentPasswordValid)
            {
                Snackbar.Add("Current password is not valid.", Severity.Error);
                return;
            }

            await UserService.UpdatePasswordAsync(_passwordChangeRequest);
            Snackbar.Add("Password updated successfully.", Severity.Success);
            NavigationManager.NavigateTo($"/User/{userId}");
        }

        // In the UserEditBase class
        protected void ToggleCurrentPasswordVisibility()
        {
            if (_currentPasswordInputType == "password")
            {
                _currentPasswordInputType = "text";
                _currentPasswordIcon = Icons.Material.Filled.Visibility;
            }
            else
            {
                _currentPasswordInputType = "password";
                _currentPasswordIcon = Icons.Material.Filled.VisibilityOff;
            }
        }

        protected void ToggleNewPasswordVisibility()
        {
            if (_newPasswordInputType == "password")
            {
                _newPasswordInputType = "text";
                _newPasswordIcon = Icons.Material.Filled.Visibility;
            }
            else
            {
                _newPasswordInputType = "password";
                _newPasswordIcon = Icons.Material.Filled.VisibilityOff;
            }
        }


        // Toggle confirm password visibility
        protected void ToggleConfirmPasswordVisibility()
        {
            if (_confirmPasswordInputType == "password")
            {
                _confirmPasswordInputType = "text";
                _confirmPasswordIcon = Icons.Material.Filled.Visibility;
            }
            else
            {
                _confirmPasswordInputType = "password";
                _confirmPasswordIcon = Icons.Material.Filled.VisibilityOff;
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

            _passwordValidationMessage = string.Join(" ", errors);

            return !errors.Any();
        }
    }
}
