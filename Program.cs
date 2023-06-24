using MySqlConnector;
using asset_amy;
using asset_amy.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;

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
builder.Services.AddScoped<ExpenseManager>();
builder.Services.AddScoped<RevenueManager>();
builder.Services.AddScoped<AssetManager>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = "JWT_or_Cookie";
    options.DefaultChallengeScheme = "JWT_or_Cookie";
})
    .AddCookie("Cookies", options => {
        options.LoginPath = "/sign-in";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    })
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("AppSettings:Token").Value!
                )
            )
        };
    })
    .AddPolicyScheme("JWT_or_Cookie", "JWT_or_Cookie", options => {
        options.ForwardDefaultSelector = context => {
            string jwt = context.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrEmpty(jwt) && jwt.StartsWith("Bearer "))
            	return "Bearer";

            return "Cookies";
        };
    });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/404";
        await next();
    }
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
