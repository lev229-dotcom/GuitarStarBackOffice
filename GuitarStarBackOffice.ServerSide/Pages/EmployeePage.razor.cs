
//using GuitarStarBackOffice.Server.Services.EmployeeService;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

namespace GuitarStarBackOffice.ServerSide.Pages;

public partial class EmployeePage
{
   IEnumerable<Employee> employees;
    [Inject] private EmployeeService EmployeeService { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

       await Get();
    }

    public async Task Get()
    {
        employees =  await EmployeeService.GetEmployees();
    }
}
