using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class OrdersBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        protected string _errorMessage = string.Empty;
        protected IEnumerable<OrderViewModel> orders { get; set; } = new List<OrderViewModel>();
        protected CancellationToken Token { get; set; } = default!;

        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected AuthService AuthService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (!await AuthService.CheckAuthenticationAsync())
                {
                    
                    NavigationManager.NavigateTo("/signin");
                    return;
                }

               /* userId = await AuthService.GetUserIdAsync(); */

               
                orders = await UserService.GetUserOrders(userId, Token);
            }
            catch (Exception ex)
            {
                _errorMessage = $"Error loading orders: {ex.Message}";
            }
            finally
            {
                _isLoading = false; // ????????? ?? ????? ??????? ??????????
            }
        }

        protected void NavigateToUserPage()
        {
            // ?????????? ????????? ???
            NavigationManager.NavigateTo($"/User/{userId}");
        }
    }
}
