using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;


namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignUpBase : ComponentBase
    {

        protected UserAddModel? _userModel { get; set; }
        protected string _errorMessage;
        [Inject] protected UserService UserService { get; set; } = default!;


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

                //  NavigationManager.NavigateTo("/success"); // Example redirect
            }
            else
            {
                _errorMessage = result.ErrorMessage;
            }
        }
    }
}