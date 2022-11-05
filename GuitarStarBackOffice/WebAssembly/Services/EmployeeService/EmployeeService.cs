using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using WebAssembly.Data;

namespace WebAssembly.Services.EmployeeService;

public class EmployeeService
{
    DataContext dataContext;
    public List<Employee> Employees { get; set; } = new List<Employee>();

    public EmployeeService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    //public async Task<ActionResult<List<Employee>>> GetEmployees()
    //{
    //    var employees = await dataContext.Employees.ToListAsync();

    //    return employees;
    //}
}
