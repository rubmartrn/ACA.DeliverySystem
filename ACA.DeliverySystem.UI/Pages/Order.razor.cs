using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ACA.DeliverySystem.UI.Pages
{
    public class OrderDetailBase : ComponentBase
    {
        [Parameter]
        public int orderId { get; set; }
        [Parameter]
        public decimal amountForPay { get; set; }

        public string? errorMessage { get; set; }

        protected OrderViewModel _orderModel { get; set; } = new OrderViewModel();

        protected CancellationToken Token { get; set; } = default!;

        [Inject]
        protected OrderService OrderService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()

        {

            try
            {
                _orderModel = await OrderService.GetById(orderId, Token);

            }
            catch (HttpRequestException m)
            {
                Console.WriteLine(m.Message);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        protected async Task CancelOrder()
        {
            try
            {
                var result = await OrderService.CancelOrder(orderId, Token);
                if (result.Success)
                {
                    NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
                }
                else
                {
                    errorMessage = "Failed to cancel the order. Please try again.";
                }
            }
            catch (HttpRequestException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception m)
            {
                errorMessage = m.Message;
            }

        }

        protected async Task RemoveItem(int orderId, int itemId)
        {
            try
            {
                var result = await OrderService.RemoveItemFromOrder(orderId, itemId);
                if (result.Success)
                {
                    NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
                }
                else
                {
                    errorMessage = "Failed to remove the item. Please try again.";
                }
            }
            catch (HttpRequestException ex)
            {
                errorMessage = ex.Message;
            }
            catch (Exception m)
            {
                errorMessage = m.Message;
            }
        }
        protected async Task DeleteOrder()
        {
            var confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this order?");
            if (confirmed)
            {
                var result = await OrderService.Delete(orderId);
                if (result.Success)
                {
                    NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
                }
                else
                {
                    errorMessage = "Failed to delete the order. Please try again.";
                }
            }
        }

        protected void GoBackToOrders()
        {
            NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
        }

        protected void AddItems()
        {
            NavigationManager.NavigateTo($"/ItemsList/{_orderModel.Id}");
        }


        protected async Task Pay()
        {
            var result = await OrderService.PayForOrder(orderId, amountForPay);
            if (result.Success)
            {
                NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
            }
            else
            {
                errorMessage = "Fail to pay.";
            }
        }
    }
}
