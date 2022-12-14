using BlazorDownloadFile;
using Blazored.LocalStorage;
using GuitarStarBackOffice.ServerSide;
using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.BackUpService;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.ServerSide.Services.ExportService;
using GuitarStarBackOffice.ServerSide.Services.HistoryService;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorDownloadFile();

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();
builder.Services.AddScoped<NorthwindService>();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<WareHouseService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<DiagramService>();
builder.Services.AddScoped<UseTemplateXLSX>();
builder.Services.AddScoped<Excel>();
builder.Services.AddScoped<ExportService>();
builder.Services.AddScoped<BackupService>();
builder.Services.AddScoped<ReloadService>();
builder.Services.AddScoped<HistoryService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthStateProvider>());
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();

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

app.Run();
