using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignInBase : ComponentBase
    {
        protected SignInModel signInModel = new SignInModel();
        protected UserService UserService { get; set; } = default!;

        protected async Task HandleSignIn()
        {
            var result = await UserService.SignIn(signInModel.Email);
            if (result.Success)
            {

                // NavigationManager.NavigateTo($"/user/{result.Data.Id}");
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