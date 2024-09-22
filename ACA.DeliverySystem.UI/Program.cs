using ACA.DeliverySystem.UI;
using ACA.DeliverySystem.UI.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var url = builder.Configuration["AcaDeliverySystemUrl"]!;

builder.Services.AddHttpClient("AcaDS",
    h => h.BaseAddress = new Uri(url));

builder.Services.AddHttpClient<OrderService>("AcaDS");

builder.Services.AddHttpClient<UserService>("AcaDS");

builder.Services.AddHttpClient<AuthService>("AcaDS");


builder.Services.AddHttpClient<ItemService>(
    h => h.BaseAddress = new Uri("https://localhost:7055")
    );



builder.Services.AddMudServices();

await builder.Build().RunAsync();
