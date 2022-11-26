using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService.Models;
using GuitarStarBackOffice.ServerSide.Services.Hash;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using Radzen;

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
        var employees = dataContext.Employees.Include(p => p.Post).ToList();

        return employees;
    }

    public async Task<Employee> Authorize(AuthenticateInputModel authenticateInputModel)
    {
        var employee = dataContext.Employees.Include(p => p.Post)
            .Where(i => i.Email == authenticateInputModel.Email &&
            i.Password == HashHelper.GetHashString(authenticateInputModel.Password))
            .FirstOrDefault();

        return await Task.FromResult(employee);
    }

    public async Task<Employee> GetEmployeeById(Guid id)
    {
        Employee employee = await dataContext.Employees.Include(d => d.Post).Where(i => i.IdEmployee == id).FirstOrDefaultAsync();

        return await Task.FromResult(employee);
    }

    public async Task UpdateEmployee(Employee employee, bool IsNewPassword = false)
    {
        if (IsNewPassword)
            employee.Password = HashHelper.GetHashString(employee.Password);
        else
            employee.Password = employee.Password;
        dataContext.Employees.Attach(employee);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddEmployee(Employee employee)
    {
        employee.Password = HashHelper.GetHashString(employee.Password);

        dataContext.Employees.Add(employee);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Employee employee)
    {
        dataContext.Employees.Remove(employee);
        await dataContext.SaveChangesAsync();
    }

    public void Reload()
    {
        foreach (var entry in dataContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
}
