using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.ServerSide.Services.ExportService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
    [Inject] private ExportService ExportService { get; set; }

    [Inject] private IJSRuntime JS { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }


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
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        await grid.Reload();
    }

    private async Task AddEmployee()
    {
        await DialogService.OpenAsync<AddEmployee>("Добавить сотрудника",null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
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
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });


        }
    }

        private async Task Export()
        {
            var downloadOptions = await ExportService.Execute("EmployeeTemplate");

            await JS.InvokeVoidAsync(downloadOptions.Identifier, downloadOptions.FileName, downloadOptions.Stream);
        }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);

    }

}
