using DataBaseService.Data;
using DataBaseService.Services.EmployeeService.Models;
using DataBaseService.Services.Hash;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace DataBaseService.Services.EmployeeService;

public class EmployeeService
{
    DataContext dataContext;
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

        dataContext.EmployeeHistories.Add(new EmployeeHistory
        {
            EventType = "Обновлен сотрудник",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")}, {Environment.NewLine}" +
            $"Сотрудник: {employee.Surname} {employee.Name} {employee.Patronymic}{Environment.NewLine}, Должность: {employee.Post.PostName}"
        });
        await dataContext.SaveChangesAsync();
    }
    public async Task AddEmployee(Employee employee)
    {
        employee.Password = HashHelper.GetHashString(employee.Password);

        dataContext.Employees.Add(employee);
        dataContext.EmployeeHistories.Add(new EmployeeHistory
        {
            EventType = "Добавлен новый сотрудник",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")} {Environment.NewLine}" +
            $"Сотрудник: {employee.Surname} {employee.Name} {employee.Patronymic} {Environment.NewLine}, Должность: {employee.Post.PostName}"
        });
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteEmployee(Employee employee)
    {
        dataContext.Employees.Remove(employee);
        dataContext.EmployeeHistories.Add(new EmployeeHistory
        {
            EventType = "Удален сотрудник",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")} {Environment.NewLine}" +
            $"Сотрудник: {employee.Surname} {employee.Name} {employee.Patronymic} {Environment.NewLine}, Должность: {employee.Post.PostName}"
        });
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
