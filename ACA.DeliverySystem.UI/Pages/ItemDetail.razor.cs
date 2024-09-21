using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ACA.DeliverySystem.UI.Pages
{
    public class ItemDetailBase : ComponentBase
    {
        [Parameter]
        public int itemId { get; set; }

        protected ItemViewModel? item;
        [Inject]
        protected ItemService ItemService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;
        [Inject]
        protected AuthService AuthService { get; set; } = default!;

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
            var userId = await AuthService.GetUserIdAsync();
            NavigationManager.NavigateTo($"User/{userId}/orders");
        }


        protected void GoBack()
        {
            NavigationManager.NavigateTo("/item");
        }
    }
}