using FinalTestProject.Components;
using FinalTestProject.Models.Accounts;
using FinalTestProject.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UbysSystemDB");

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContextFactory<UbysSystemDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddSingleton<UserData>();

// HttpContextAccessor servisini ekleyin
builder.Services.AddHttpContextAccessor();

// Register Session State service
builder.Services.AddScoped<FinalTestProject.Services.SessionState>();

//Logout servisinin eklenmesi
builder.Services.AddScoped<FinalTestProject.Services.LogoutService>();


// Session servisini ekleyin
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddBlazorBootstrap();


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);

    // Add HSTS middleware for production scenarios
    app.UseHsts();
}

// Add HTTPS Redirection Middleware
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Session'i kullanilabilir hale getirin
app.UseSession();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(FinalTestProject.Client._Imports).Assembly);

app.Run();