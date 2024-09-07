using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class ItemForUserBase : ComponentBase
    {
        protected IEnumerable<ItemViewModel> items;

        [Parameter]
        public int orderId { get; set; }
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
                items = await ItemService.GetAll();
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Error loading items: {ex.Message}", Severity.Error);
            }
        }

        protected void GoToItemDetail(int itemId)
        {
            NavigationManager.NavigateTo($"/itemForUser/{itemId}");
        }

        protected async Task OrderItem(ItemViewModel item)
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
    }
}