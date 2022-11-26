using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using GuitarStarBackOffice.Shared.Validators;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.ServerSide.Pages.ProfileDirectory;

public partial class ProfileEditor
{
    [Inject]
    protected DialogService DialogService { get; set; }
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] private CustomAuthStateProvider UserSession { get; set; }


    [Parameter]
    public Guid cuurentAccountId { get; set; }

    [Required(ErrorMessage = "Обязательное поле")]
    [MinLength(6, ErrorMessage = "Минимальная длина 6 символов")]
    [MaxLength(26, ErrorMessage = "Максимальная длина 26 символов")]
    public string Password { get; set; }

    Employee employee = new ();

    EditContext editContext;

    private bool IsActive =>
       CheckPasswordIsValid() && EmailValidator.CheckIsValidEmail(employee.Email);

    private bool CheckPasswordIsValid()
    {
        if (string.IsNullOrWhiteSpace(Password))
            return true;
        return Password.Length > 6 && Password.Length < 26;
    }

    protected override async Task OnInitializedAsync()
    {
        employee = await EmployeeService.GetEmployeeById(cuurentAccountId);
       // editContext = new EditContext(employee);
       // editContext.OnFieldChanged += FieldChanged;
    }
    //private void FieldChanged(object sender, FieldChangedEventArgs args)
    //{
    //    IsActive = !editContext.Validate();
    //}
    private async Task HandleEdit()
    {
        if(!string.IsNullOrWhiteSpace(Password))
            employee.Password = Password;
        await EmployeeService.UpdateEmployee(employee);
        await UserSession.StartSession(UserSession.FullName, UserSession.Username, UserSession.Role);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
