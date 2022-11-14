using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services.DiagramModels;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Services;

public class DiagramService
{
    DataContext dataContext;

	public DiagramService(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}

	public async Task<IEnumerable<Order>> GetOrdersByDate(DateTime OrderDate)
	{
		var orders =  dataContext.Orders.Where(d => d.OrderDate == OrderDate).ToList();

		return orders;
	}
}
