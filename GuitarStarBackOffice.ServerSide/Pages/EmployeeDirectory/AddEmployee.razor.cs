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

public partial class AddEmployee
{
    [Inject]
    protected NavigationManager NavigationManager { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] private PostService PostService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Employee newEmployee = new Employee();

    public List<EmployeeRoleDdlModel> UserRoles = new ();

    public IEnumerable<Post> posts ;

    EditContext editContext;

    private bool IsActive = true;

    protected override async Task OnInitializedAsync()
    {
        posts = await PostService.GetPosts();
        editContext = new EditContext(newEmployee);
        editContext.OnFieldChanged += FieldChanged;
    }

    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
    }

    public AddEmployee()
    {
        foreach (Role roleEnumValue in Enum.GetValues(typeof(Role)))
            UserRoles.Add(new EmployeeRoleDdlModel
            {

                UserRoleValue = roleEnumValue,
                RoleName = roleEnumValue.ToDescriptionString(),
            });
    }
   
    private async Task HandleAdd()
    {
        if (editContext.Validate())
        {
            try
            {
                newEmployee.AccountCreateDate = DateTime.Now;
                await EmployeeService.AddEmployee(newEmployee);
                await Close(null);
                ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
            }
            catch (Exception ex)
            {
                ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = "Данные не валидны", Duration = 4000 });
            }
        }
        else
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{Environment.NewLine}Данные не валидны", Duration = 4000 });
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
