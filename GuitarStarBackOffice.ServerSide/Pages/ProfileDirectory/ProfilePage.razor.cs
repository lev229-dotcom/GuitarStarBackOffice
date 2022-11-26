using Blazored.LocalStorage;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;

namespace GuitarStarBackOffice.ServerSide.Pages.ProfileDirectory;

public partial class ProfilePage
{
    [Inject] private ILocalStorageService LocalStorageService { get; set; }

    [Inject] private CustomAuthStateProvider CustomAuthStateProvider { get; set; }
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] private CustomAuthStateProvider UserSession { get; set; }
    [Inject] private DialogService DialogService { get; set; }


    // [Parameter] public string id { get; set; }


    Employee? currentEmployee;
    protected override async Task OnInitializedAsync()
    {
        var id = await LocalStorageService.GetItemAsync<string>("admin.fullname");
        if(id is not null)
        currentEmployee = await EmployeeService.GetEmployeeById(Guid.Parse(id));
    }

    private async Task OpenAccountEditor()
    {
        await DialogService.OpenAsync<ProfileEditor>("Изменение данных профиля", new Dictionary<string, object>()
        { {"cuurentAccountId", currentEmployee.IdEmployee}},
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        currentEmployee = await EmployeeService.GetEmployeeById(Guid.Parse(UserSession.FullName));
    }
}
