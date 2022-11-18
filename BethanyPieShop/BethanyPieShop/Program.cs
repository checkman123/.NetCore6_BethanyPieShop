using BethanyPieShop.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*  Put Dependency Injection Here aka Register Dependent Services   
        ConfigureServices()*/
//builder.Services.AddAuthentication();

//make it so it is Development mode
builder.Environment.IsDevelopment();

//Connect with sql server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ShoppingCart>(serviceProvider => ShoppingCart.GetCart(serviceProvider));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();



var app = builder.Build();

/*  Middleware Pipeline 
        */
//app.UseAuthentication();

//Check if it is in development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseHttpsRedirection();
app.UseStatusCodePages();
app.UseStaticFiles();

//Need to call session before routing
app.UseSession();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapGet("/", () => "Hello World!");

app.Run();
