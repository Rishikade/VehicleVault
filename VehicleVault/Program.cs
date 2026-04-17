using Microsoft.EntityFrameworkCore;
using VehicleVault.Data;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build(); // ⚠️ IMPORTANT (pehle declare)

// Middleware
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // session after routing

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run(); // last line