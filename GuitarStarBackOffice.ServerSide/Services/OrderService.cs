using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Services;

public class OrderService
{
    DataContext dataContext;

    private double TotalOrderAmount;

    public List<Order> Orders { get; set; } = new();

    public OrderService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }


    #region OrderElements

    public async Task<IEnumerable<OrderElement>> GetOrderElements(Guid orderId)
    {
        var orders = dataContext.OrderElements.Include(o => o.Order)
            .Include(p => p.Product).ThenInclude(c => c.Category).Where(e => e.OrderId == orderId).ToList();

        return orders;
    }

    public async Task AddElement(OrderElement orderElement)
    {
        dataContext.OrderElements.Add(orderElement);
        await dataContext.SaveChangesAsync();
    }
    public async Task UpdateElement(OrderElement orderElement)
    {
        dataContext.OrderElements.Attach(orderElement);
        await dataContext.SaveChangesAsync();
    }
    public async Task DeleteElement(OrderElement orderElement)
    {
        dataContext.OrderElements.Remove(orderElement);
        await dataContext.SaveChangesAsync();
    }

    #endregion OrderElements

    public async Task<IEnumerable<Order>> GetOrders()
    {
        var orders = dataContext.Orders.Include(e => e.Employee).ToList();

        return orders;
    }

    public async Task<int> GetLastOrder()
    {
        var lastOrder = dataContext.Orders.OrderBy(n => n.OrderNumber).Select(n => n.OrderNumber).LastOrDefault() + 1;
        return lastOrder;
    }

    public async Task<double> GetOrderTotalAmount(Guid id)
    {
        var OrderElements = dataContext.OrderElements.Where(i => i.OrderId == id).ToList();

        foreach (var item in OrderElements)
        {
            var product = await dataContext.Products.Where(i => i.IdProduct == item.ProductId).FirstOrDefaultAsync();

            item.Product = product;

            TotalOrderAmount += item.Product.ProductPrice;

        }

        return TotalOrderAmount;
    }
    public async Task<Order> GetOrderById(Guid id)
    {
        Order order = await dataContext.Orders.Where(i => i.IdOrder == id).FirstOrDefaultAsync();

        return await Task.FromResult(order);
    }

    public async Task UpdateOrder(Order order)
    {
        dataContext.Orders.Attach(order);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddOrder(Order orders)
    {
        dataContext.Orders.Add(orders);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteOrder(Order order)
    {
        dataContext.Orders.Remove(order);
        await dataContext.SaveChangesAsync();
    }
}
