using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        protected UserViewModel? _userModel { get; set; }
        protected OrderAddModel? _orderModel { get; set; }
        protected string? _errorMessage;
        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized()
        {
            _userModel = new UserViewModel();
            _orderModel = new OrderAddModel();
            _errorMessage = string.Empty;
        }


        protected override async Task OnParametersSetAsync()
        {
            var result = await UserService.GetUserById(userId);
            if (result != null)
            {
                _userModel = result;
            }
            else
            {
                _errorMessage = "User not found.";
            }
        }

        protected async Task HandleAddOrder()
        {
            var result = await UserService.AddOrderInUser(userId, _orderModel!);
            if (result.Success)
            {
                NavigationManager.NavigateTo($"User/orders");
            }
            else
            {
                _errorMessage = result.ErrorMessage;
            }

        }

        protected void EditUser()
        {
            NavigationManager.NavigateTo($"/User/Edit/{userId}");
        }

        protected void GoToUserOrders()
        {
            if (_userModel != null)
            {
                NavigationManager.NavigateTo($"User/{userId}/orders");
            }
        }

    }
}