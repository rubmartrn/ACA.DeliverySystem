using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserSignInBase : ComponentBase
    {
        protected SignInModel signInModel = new SignInModel();

        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        protected async Task HandleSignIn()
        {
            try
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
            catch (HttpRequestException ex)
            {
                Snackbar.Add("The user not founded", Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add("The user not founded", Severity.Error);
            }

        }

        protected class SignInModel
        {
            public string? Email { get; set; }
        }
    }
}