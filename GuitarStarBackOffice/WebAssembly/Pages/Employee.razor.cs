
//using GuitarStarBackOffice.Server.Services.EmployeeService;
using Microsoft.AspNetCore.Components;

namespace WebAssembly.Pages;

public partial class Employee
{
    IEnumerable<Employee> employees;
    //[Inject] private EmployeeService EmployeeService { get; set; }

    //protected override async void OnInitialized()
    //{
    //    base.OnInitialized();

    //    employees = await EmployeeService.GetEmployees();
    //}
}
