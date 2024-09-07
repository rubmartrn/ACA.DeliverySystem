using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class ItemDetailForUserBase : ComponentBase
    {
        [Parameter]
        public int itemId { get; set; }
        [Parameter]
        public int orderId { get; set; }

        protected ItemViewModel? item;
        [Inject]
        protected ItemService ItemService { get; set; } = default!;

        [Inject]
        protected OrderService OrderService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                item = await ItemService.GetbyId(itemId);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error loading item: {ex.Message}", Severity.Error);
            }
        }

        protected async Task OrderItem()
        {
            var response = await OrderService.AddItemInOrder(orderId, item.Id);
            if (response.Success)
            {
                Snackbar.Add($"Ordering {item.Name}...", Severity.Success);

            }
            else
            {
                Snackbar.Add($"Failed to order {item.Name}: {response.ErrorMessage}", Severity.Error);
            }
        }

        protected void GoBack()
        {
            NavigationManager.NavigateTo($"/ItemsList/{orderId}");
        }
    }
}