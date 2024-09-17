using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class ItemsForUserBase : ComponentBase
    {
        protected IEnumerable<ItemViewModel>? items;

        [Parameter]
        public int orderId { get; set; }

        [Parameter]

        public int itemId { get; set; }

        [Inject]
        protected ItemService ItemService { get; set; } = default!;

        [Inject]
        protected OrderService OrderService { get; set; } = default!;
        [Inject]
        protected AuthService AuthService { get; set; } = default!;

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
            NavigationManager.NavigateTo($"ItemDetailForUser/{itemId}/{orderId}");
        }

        protected async void OrderItem(ItemViewModel item)
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
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

        protected async Task GoBackToUserOrder()
        {
            if (!await AuthService.CheckAuthenticationAsync())
            {

                NavigationManager.NavigateTo("/signin");
                return;
            }
            NavigationManager.NavigateTo($"/Order/{orderId}");
        }
    }
}