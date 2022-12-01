using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.HistoryService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.HistoryDirectory;

public partial class EmployeeHistories
{
    private RadzenDataGrid<EmployeeHistory> grid;

    IEnumerable<Category> categories;
    IEnumerable<EmployeeHistory> employee;

    [Inject] public HistoryService HistoryService { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        //grid.Data = await HistoryService.GetEmployeesHistories();
        employee = await HistoryService.GetEmployeesHistories();
        await grid.Reload();
        StateHasChanged();
        await grid.Reload();
    }

}
