using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.EmployeeDirectory;

public partial class EmployeePage
{
    private RadzenDataGrid<Employee>? grid;

    IEnumerable<Employee> employees;
    [Inject] private EmployeeService EmployeeService { get; set; }
    
    [Inject] private DialogService DialogService { get; set; }

    [Inject] private NorthwindService service { get; set; }

    private EmployeeEditor employeeEditor;
    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        employees = await EmployeeService.GetEmployees();
    }

    private async void OpenEditor(Employee editedEmployee)
    {
        await DialogService.OpenAsync<EmployeeEditor>("Редактировать сотрудника", new Dictionary<string, object>() 
               { { "editedEmployeeId", editedEmployee.IdEmployee } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    private async Task AddEmployee()
    {
        await DialogService.OpenAsync<AddEmployee>("Добавить сотрудника",null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        employees = await EmployeeService.GetEmployees();

        await grid.Reload();
        //EmployeeService.Reload();

    }
     
    private async Task DeleteEmployee(Employee deletedEmployee)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
       if( await DialogService.Confirm("Вы действительно хотите удалить запись?", 
            "Удаление записи из базы данных", options) == true)
        {
           await EmployeeService.DeleteEmployee(deletedEmployee);

            employees = await EmployeeService.GetEmployees();

            await grid.Reload();

        }
    }

    public void Export(string type)
    {
        service.Export("OrderDetails", type, new Query()
        {
            OrderBy = grid.Query.OrderBy,
            Filter = grid.Query.Filter,
            Select = string.Join(",", grid.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
        });
    }
}
