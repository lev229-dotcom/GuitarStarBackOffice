using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
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

    public string EmpName { get; set; }

    [Parameter]
    public Guid editedEmployeeId { get; set; }
    Employee editedEmployee = new Employee();

    public bool IsOpened { get; set; } = false;

    public List<EmployeeRoleDdlModel> UserRoles = new();
    public IEnumerable<Post> posts;


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
    }
    public async Task Open(Employee employeeModel)
    {
        IsOpened = true;

        EmpName = employeeModel.Name;
    }

    private async Task HandleEdit()
    {
        await EmployeeService.UpdateEmployee(editedEmployee);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
