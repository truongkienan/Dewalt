using Dewalt;
using Dewalt.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(p =>
{
    p.ExpireTimeSpan = TimeSpan.FromDays(30);
    p.LoginPath = "/auth/login";
    p.AccessDeniedPath = "/auth/accessdenied";
    p.Events.OnRedirectToLogin = context =>
    {
        if (Helper.IsAdminContext(context))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/dashboard/auth/login" + redirectPath.Query);
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };

    p.Events.OnRedirectToAccessDenied = context =>
    {
        if (Helper.IsAdminContext(context))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/dashboard/auth/accessdenied" + redirectPath.Query);
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };

});

builder.Services.AddScoped(p => new SiteProvider(builder.Configuration));
builder.Services.AddScoped(p => new CartFilter(p.GetRequiredService<SiteProvider>()));
builder.Services.AddScoped(p => new CategoryFilter(p.GetRequiredService<SiteProvider>()));
builder.Services.AddScoped(p => new BrandFilter(p.GetRequiredService<SiteProvider>()));
builder.Services.AddScoped(p => new ProductStatusFilter(p.GetRequiredService<SiteProvider>()));
var app = builder.Build();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapControllerRoute(name: "dashboard", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");
app.Run();
