using ACA.DeliverySystem.UI.Models;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ACA.DeliverySystem.UI.Pages
{
    public class UserBase : ComponentBase
    {
        [Parameter]
        public int userId { get; set; }

        protected UserViewModel _userModel;
        protected string _errorMessage;
        [Inject] protected UserService UserService { get; set; } = default!;

        protected override void OnInitialized()
        {
            _userModel = new UserViewModel();
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
    }
}