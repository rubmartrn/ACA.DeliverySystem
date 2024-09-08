using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class PaymentBase : ComponentBase
    {
        [Parameter] public int orderId { get; set; }
        protected decimal amountForPay;
        protected string? cardNumber;
        protected string? expiryDate;
        protected string? cvv;
        protected string? errorMessage;
        [Inject]
        protected OrderService OrderService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        protected async Task Pay()
        {
            if (string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
            {
                errorMessage = "Please fill in all card details.";
                return;
            }

            var result = await OrderService.PayForOrder(orderId, amountForPay);
            if (result.Success)
            {
                NavigationManager.NavigateTo($"Order/{orderId}");
            }
            else
            {
                Snackbar.Add(result.ErrorMessage, Severity.Error);
            }

        }
    }
}