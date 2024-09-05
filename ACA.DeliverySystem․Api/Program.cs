using ACA.DeliverySystem.Business.MappingProfiles;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using ACA.DeliverySystem_Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();

var settings = builder.Configuration.Get<AppSettings>();
builder.Services.AddDbContext<DeliveryDbContext>(
options => options.UseSqlServer(settings.EntitySettings.ACAdb));

builder.Services.AddAutoMapper(typeof(ItemProfileDTO).Assembly);
builder.Services.AddAutoMapper(typeof(ItemProfile).Assembly);



var app = builder.Build();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.Run();


