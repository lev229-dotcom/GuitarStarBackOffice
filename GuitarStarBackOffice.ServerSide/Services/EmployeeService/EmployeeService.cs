using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Services.EmployeeService;

public class EmployeeService
{
    DataContext dataContext;
    public List<Employee> Employees { get; set; } = new List<Employee>();

    public EmployeeService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        var employees =  dataContext.Employees.ToList();

        return employees;
    }
}
