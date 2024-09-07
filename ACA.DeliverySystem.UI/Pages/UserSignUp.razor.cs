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
        [Inject] protected UserService UserService { get; set; } = default!;
        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        protected override void OnInitialized()
        {
            _userModel = new UserAddModel();
            _errorMessage = string.Empty;
        }
        protected async Task HandleSubmit()
        {
            var result = await UserService.Create(_userModel);
            if (result.Success)
            {
                NavigationManager.NavigateTo("/signin");
            }
            else
            {
                Snackbar.Add("This email is already registered.", Severity.Error);
                _errorMessage = result.ErrorMessage;
            }
        }
    }
}