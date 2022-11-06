using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;
using Radzen.Blazor;
using System.ComponentModel;

namespace GuitarStarBackOffice.ServerSide.Pages.EmployeeDirectory;

public partial class AddEmployee
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private EmployeeService EmployeeService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Employee newEmployee = new Employee();

    public List<EmployeeRoleDdlModel> UserRoles = new ();

    public AddEmployee()
    {
        foreach (Role roleEnumValue in Enum.GetValues(typeof(Role)))
            UserRoles.Add(new EmployeeRoleDdlModel
            {

                UserRoleValue = roleEnumValue,
                RoleName = roleEnumValue.ToDescriptionString(),
            });
    }
    protected override async Task OnInitializedAsync()
    {

    }

    private async Task HandleAdd()
    {
        try
        {
            newEmployee.AccountCreateDate = DateTime.Now;
            await EmployeeService.AddEmployee(newEmployee);
            await Close(null);
        }
        catch(Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = "position: absolute; ", Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
        }
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
        
    }
}
