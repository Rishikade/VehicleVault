using Microsoft.EntityFrameworkCore;
using VehicleVault.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllersWithViews();

// Add DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Session
builder.Services.AddSession();

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();

// Enable Session
app.UseSession();

// Default route → Login page first
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();