using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class OrderDetailBase : ComponentBase
    {
        [Parameter]
        public int orderId { get; set; }
        [Parameter]
        public decimal amountForPay { get; set; }

        protected OrderViewModel _orderModel { get; set; } = new OrderViewModel();


        [Inject]
        protected OrderService OrderService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {

            try
            {
                _orderModel = await OrderService.GetById(orderId);

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
            var result = await OrderService.CancelOrder(orderId);
            if (result.Success)
            {
                NavigationManager.NavigateTo($"/User/{_orderModel.UserId}/orders");
            }
            else
            {
                // Handle error message
            }
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
                // Handle error message
            }
        }
    }
}
