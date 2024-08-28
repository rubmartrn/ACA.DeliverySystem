using ACA.DeliverySystem.UI;
using ACA.DeliverySystem.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

/*builder.Services.AddScoped<BackendService>();*/
builder.Services.AddScoped<ItemService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
