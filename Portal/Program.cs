using Blazored.LocalStorage;
using Blazored.Toast;
using DataBaseService.Data;
using DataBaseService.Services;
using DataBaseService.Services.ClientService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Portal.Authorization;
using Portal.Data;
using Portal.TelegramBotService;
using Radzen;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")),
        ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<WishlistElementsService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddTransient<ConfirmOrderCommand>();
builder.Services.AddTransient<CreateEmailBodyRule>();

TelegramBotClient botClient = new TelegramBotClient("6195338326:AAGBj1e9kEF4GagQUFD4rwVt_Xdu10WzLfQ");
builder.Services.AddSingleton<ITelegramBotClient>(botClient);
builder.Services.AddHostedService<Bot>();


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
