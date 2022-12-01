using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Services.HistoryService;

public class HistoryService
{
    DataContext dataContext;

	public HistoryService(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}

    public async Task<IEnumerable<EmployeeHistory>> GetEmployeesHistories()
    {
        var employees = await dataContext.EmployeeHistories.ToListAsync();

        return employees;
    }
    public async Task<IEnumerable<ProductHistory>> GetProductsHistories()
    {
        var productHistories = await dataContext.ProductHistories.ToListAsync();

        return productHistories;
    }

}
