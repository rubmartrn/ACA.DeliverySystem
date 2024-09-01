using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignInBase : ComponentBase
    {
        protected SignInModel signInModel = new SignInModel();

        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected async Task HandleSignIn()
        {
            var result = await UserService.SignIn(signInModel.Email);
            if (result.Success)
            {

                NavigationManager.NavigateTo($"/User/{result.Data.Id}");
            }
            else
            {

                Console.WriteLine(result.ErrorMessage);
            }
        }

        protected class SignInModel
        {
            public string? Email { get; set; }
        }
    }
}