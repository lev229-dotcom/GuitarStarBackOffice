using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services.DiagramModels;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Services;

public class DiagramService
{
    DataContext dataContext;

	public double Amount { get; set; }

	public DiagramService(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}

	public async Task<IEnumerable<DiagramServiceModel>> GetOrdersByDate(DateTime StartDate, DateTime SecondDate)
	{
		var orders =  dataContext.Orders.Where(d => d.OrderDate >= StartDate && d.OrderDate < SecondDate).ToList();

		var days = new List<DateTime>();
		
		for(DateTime date = StartDate; date <= SecondDate; date = date.AddDays(1))
			days.Add(date);

		var ordersDates = new List<DateTime>();
		
		foreach(var order in orders)
			ordersDates.Add(order.OrderDate);
		var resultDates = ordersDates.Intersect(days);

		var diagramServiceModel = new List<DiagramServiceModel>();

		foreach (var item in resultDates)
		{
			var ordersInCurrentDate = dataContext.Orders.Where(d => d.OrderDate == item.Date).ToList();

			foreach (var order in ordersInCurrentDate)
			{
                Amount += order.TotalOrderAmount;
			}
			diagramServiceModel.Add(new DiagramServiceModel { Date = item.Date, AmountForDate = Math.Round(Amount, 0) });
			Amount = 0;
        }

        return diagramServiceModel;
	}
}
