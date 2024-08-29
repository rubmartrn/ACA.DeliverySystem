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

        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected List<OrderViewModel> orders = default!;

        protected override async Task OnInitializedAsync()
        {
            var result = await UserService.GetUserOrders(userId);
            if (result != null)
            {
                orders = result.ToList();
            }
            else
            {
                _errorMessage = "Something went wrong...";
            }
        }
    }
}