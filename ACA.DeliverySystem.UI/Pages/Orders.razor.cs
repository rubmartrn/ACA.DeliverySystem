using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class OrdersBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }
        protected string _errorMessage = default!;
        protected IEnumerable<OrderViewModel> orders { get; set; } = new List<OrderViewModel>();
        protected CancellationToken Token { get; set; } = default!;

        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                orders = await UserService.GetUserOrders(userId, Token);
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected void NavigateToUserPage()
        {
            NavigationManager.NavigateTo($"/User/{userId}");
        }
    }
}