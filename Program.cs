using MySqlConnector;
using asset_amy;
using asset_amy.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<asset_amy.DbContext.AssetAmyContext>(
    options =>
    {
        var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseMySql(
            connetionString,
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.34-mysql")
        );
    }
);
builder.Services.AddScoped<UserManager>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
