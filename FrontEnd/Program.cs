using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using MathApp.FrontEnd.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
builder.Services.AddScoped<IUnitService,UnitService>();
builder.Services.AddScoped<IEdLevelService, EdLevelService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IDefinitionService, DefinitionService>();
builder.Services.AddScoped<IIncorrectService, IncorrectService>();
builder.Services.AddScoped<IPagesService, PagesService>();
builder.Services.AddScoped<IUserProgressService, UserProgressService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/Log";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/AccesDenied";
        });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
