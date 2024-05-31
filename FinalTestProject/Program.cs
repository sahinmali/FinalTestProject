using FinalTestProject.Components;
using FinalTestProject.Models.Accounts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var dbPath = Path.Combine("Data", "UbysSystem.db");

builder.Services.AddDbContext<UbysSystemDbContext>(options =>
	options.UseSqlite($"Data Source={dbPath}")); // Log to console);

builder.Services.AddScoped<DatabaseHandler>();

builder.Services.AddSingleton<UserData>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(FinalTestProject.Client._Imports).Assembly);

app.Run();