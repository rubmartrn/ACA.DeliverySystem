using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserEditBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        protected UserUpdateModel _userModel { get; set; } = new UserUpdateModel();
        [Inject]
        protected UserService UserService { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var user = await UserService.GetUserById(userId);
            _userModel.Name = user.Name;
            _userModel.SurName = user.SurName;
        }

        protected async Task HandleEditUser()
        {
            await UserService.Update(userId, _userModel);

            NavigationManager.NavigateTo($"/User/{userId}");
        }
    }
}