using BLL.IServices;
using BLL.Services;
using Microsoft.Extensions.Options;
using WebApplication1.Mappings;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<IServiceSuperAdmin, ServiceSuperAdmin>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

// Налаштування логуваня
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.LoginPath = "/Account/Login";
        config.LogoutPath = "/Account/Logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Account}/{action=Login}/");

app.Run();
