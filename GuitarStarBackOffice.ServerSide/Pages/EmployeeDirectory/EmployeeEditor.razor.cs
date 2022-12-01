using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.EmployeeDirectory;

public partial class EmployeeEditor
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }
    [Inject] private PostService PostService { get; set; }


    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] protected NotificationService NotificationService { get; set; }


    public string EmpName { get; set; }

    [Parameter]
    public Guid editedEmployeeId { get; set; }
    Employee editedEmployee = new Employee();

    public bool IsOpened { get; set; } = false;

    public List<EmployeeRoleDdlModel> UserRoles = new();
    public IEnumerable<Post> posts;


    EditContext editContext;
    private bool IsActive = false;

    public EmployeeEditor()
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
        posts = await PostService.GetPosts();
        editedEmployee = await EmployeeService.GetEmployeeById(editedEmployeeId);
        EmpName = editedEmployee.Name;
        await Test();
    }

    private async Task Test()
    {
        editContext = new EditContext(editedEmployee);
        editContext.OnFieldChanged += FieldChanged;

    }

    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
    }
    public async Task Open(Employee employeeModel)
    {
        IsOpened = true;

        EmpName = employeeModel.Name;
    }

    private async Task HandleEdit()
    {
        try
        {

            await EmployeeService.UpdateEmployee(editedEmployee);
            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = "Данные не валидны", Duration = 4000 });
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
