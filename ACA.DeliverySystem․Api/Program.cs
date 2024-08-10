using ACA.DeliverySystem.Business.MappingProfiles;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IItemService, ItemService>();


builder.Services.AddDbContext<DeliveryDbContext>(
options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ACA.DS"));

builder.Services.AddAutoMapper(typeof(ItemProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
//builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);


var app = builder.Build();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapControllers();
app.Run();


