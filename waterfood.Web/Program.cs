using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using waterfood.Core.DependencyInjections;
using waterfood.Core.Objects.Externals;
using waterfood.Core.Services;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WaterFoodContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TehraniConnection"));
});

builder.Services.AddAllServices();



builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "WaterFood.Cookie";
                    config.LoginPath = "/Login";
                    config.LogoutPath = "/logout";
                    config.ExpireTimeSpan = TimeSpan.FromHours(10);
                });

var section = builder.Configuration.GetSection("ReCaptcha");
var secrets = new ReCaptchaSettings()
{
    SecretKey = section["SecretKey"],
    SiteKey = section["SiteKey"]
};

builder.Services.AddSingleton(secrets);

builder.Services.Configure<ReCaptchaSettings>(builder.Configuration.GetSection("ReCaptcha"));

var app = builder.Build();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/404");
    }
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseStaticFiles();

app.UseRouting();

// who are you?
app.UseAuthentication();

// are you allowed?
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.Run();
