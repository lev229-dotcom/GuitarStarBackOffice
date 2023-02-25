using DataBaseService.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DataBaseService.Services;

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

    private static List<OrderElement> OrderElements;

    private List<OrderElement> getCurrentList()
    {
        if (OrderElements is null)
            OrderElements = new List<OrderElement>();
        return OrderElements;
    }

    public async Task AddElementInMemory(OrderElement orderElement)
    {
        orderElement.Product = await dataContext.Products.Where(i => i.IdProduct == orderElement.ProductId).Include(w => w.WareHouse).Include(i => i.Category).Include(f => f.FileImage).FirstOrDefaultAsync();
        orderElement.ProductId = orderElement.ProductId;
        var orderElements = getCurrentList();

        orderElements.Add(orderElement);
    }

    public async Task RemoveProductInCart(OrderElement productId)
    {
        var orderElements = getCurrentList();

        orderElements.Remove(productId);
    }

    public async Task<int> CreateOrderTest(string fullAddress, string CustomerName, string CusomerNumber, string CustomerEmail, decimal totalPrice)
    {
        var orderElements = getCurrentList();
        var number = await GetLastOrder();
        var order = new Order
        {
            payementStatus = PayementStatus.NotPayed,
            orderStatus = OrderStatus.Accepted,
            TotalOrderAmount = (double)totalPrice,
            OrderDate = DateTime.UtcNow,
            OrderNumber = number,
            //OrderElements = orderElements,
            EmployeeId = Guid.Parse("CAF7B5FF-56CF-40D7-93D2-685191C8774A"),
            CustomerName = CustomerName,
            CustomerEmail = CustomerEmail,
            CustomerNumber = CusomerNumber,
            CustomerAddress = fullAddress
        };
        order.IdOrder = Guid.NewGuid();
        foreach (var item in orderElements)
        {
            item.Product = null;
            item.OrderId = order.IdOrder;
        }
        order.OrderElements = orderElements;
        dataContext.Orders.Add(order);
        await dataContext.SaveChangesAsync();
        return order.OrderNumber;
    }


    public async Task<List<OrderElement>> GetElementsInMemory() {
        var orderElements = getCurrentList();
        return orderElements;
    }

    private Guid orderNumber;

    public Guid getOrderNumber()
    {
        if (orderNumber == Guid.Empty)
            orderNumber = Guid.NewGuid();
        return orderNumber;
    }


    public async Task<IEnumerable<OrderElement>> GetOrderElements(Guid orderId)
    {
        var orders = dataContext.OrderElements.Include(o => o.Order)
            .Include(p => p.Product).ThenInclude(c => c.Category).Where(e => e.OrderId == orderId).ToList();

        return orders;
    }

    public async Task<OrderElement> GetOrderElementById(Guid currentOrderElement)
    {
        OrderElement orderElement = await dataContext.OrderElements.Include(o => o.Order).Where(i => i.IdOrderElement == currentOrderElement).FirstOrDefaultAsync();

        return await Task.FromResult(orderElement);
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

    public async Task<double> GetOrderTotalAmount(Guid orderId)
    {
        TotalOrderAmount = 0;
        var OrderElements = dataContext.OrderElements.Where(i => i.OrderId == orderId).ToList();

        foreach (var item in OrderElements)
        {
            var product = await dataContext.Products.Where(i => i.IdProduct == item.ProductId).FirstOrDefaultAsync();

            item.Product = product;

            TotalOrderAmount += item.Product.ProductPrice * item.ElementsCount;

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
