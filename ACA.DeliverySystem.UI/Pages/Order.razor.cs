using ACA.DeliverySystem.UI.Attributes;
using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class OrderDetailBase : ComponentBase
    {
        [Parameter]
        public int orderId { get; set; }

        [Parameter]

        public int ItemId { get; set; }
        [Parameter]
        public decimal amountForPay { get; set; }

        public string? errorMessage { get; set; }

        protected OrderViewModel? _orderModel { get; set; }

        protected CancellationToken Token { get; set; } = default!;

        [Inject]
        protected OrderService OrderService { get; set; } = default!;
        
        [Inject]
        protected AuthService AuthService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;


        protected override async Task OnInitializedAsync()

        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }

            _orderModel = new OrderViewModel();

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

            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
            var result = await OrderService.CancelOrder(orderId, Token);
            if (result.Success)
            {
                Snackbar.Add("Order canceled", Severity.Success);
                NavigationManager.NavigateTo($"/User/{_orderModel!.UserId}/orders");

            }
            else
            {
                Snackbar.Add($"{result.ErrorMessage}", Severity.Error);
            }

        }

        protected async Task RemoveItem(int orderId, int itemId)
        {

            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
            var result = await OrderService.RemoveItemFromOrder(orderId, itemId);
            if (result.Success)
            {
                Snackbar.Add("Item removed", Severity.Success);
                NavigationManager.NavigateTo($"Order/{orderId}", true);
            }
            else
            {
                Snackbar.Add($"{result.ErrorMessage}", Severity.Error);
            }

        }
        protected async Task DeleteOrder()
        {

            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
            var confirmed = await JSRuntime!.InvokeAsync<bool>("confirm", "Are you sure you want to delete this order?");
            if (confirmed)
            {
                var result = await OrderService.Delete(orderId);
                if (result.Success)
                {
                    NavigationManager.NavigateTo($"/User/{_orderModel!.UserId}/orders");
                    Snackbar.Add("Order deleted", Severity.Success);
                }
                else
                {
                    Snackbar.Add($"{result.ErrorMessage}", Severity.Warning);
                }
            }
        }

        protected async Task  GoBackToOrders()
        {

            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
            NavigationManager.NavigateTo($"/User/{_orderModel!.UserId}/orders");
        }

        protected async Task AddItems()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            if (_orderModel!.ProgressEnum != ProgressEnum.Created)
            {
                Snackbar.Add($"You can't add new items. Order is {_orderModel.ProgressEnum}", Severity.Info);
            }
            else
            {
                NavigationManager.NavigateTo($"/ItemsList/{_orderModel.Id}");
            }
        }

        protected async Task GoToItemDetail(int itemId)
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            NavigationManager.NavigateTo($"ItemDetailForUser/{itemId}/{orderId}");
        }

        protected async Task GoToPayment()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            if (_orderModel!.ProgressEnum != ProgressEnum.Created)
            {
                Snackbar.Add($"Order is {_orderModel.ProgressEnum}", Severity.Warning);
            }
            else if (_orderModel.AmountToPay == 0)
            {
                Snackbar.Add("First add items.", Severity.Info);
            }
            else
            {
                NavigationManager.NavigateTo($"/Order/{orderId}/pay");
            }
        }

        protected async Task MarkAsCompleted()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {
                NavigationManager.NavigateTo("/signin");
                return;
            }
            var result = await OrderService.OrderCompleted(orderId);
            if (result.Success)
            {
                Snackbar.Add($"{_orderModel!.Name} order marked as completed", Severity.Success);
                NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
            }
            else
            {
                Snackbar.Add($"{result.ErrorMessage}", Severity.Error);
            }
        }
    }
}
