using DataBaseService.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using Portal.TelegramBotService;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata.Ecma335;
using Telegram.Bot;

namespace DataBaseService.Services;

public class OrderService
{
    DataContext dataContext;
    private readonly ITelegramBotClient bot;

    private double TotalOrderAmount;

    public List<Order> Orders { get; set; } = new();

    public OrderService(DataContext dataContext, ITelegramBotClient bot)
    {
        this.dataContext = dataContext;
        this.bot = bot;
    }

    public async Task<(byte[] Data, string FileName, string Extension)> GetFileById(Guid id)
    {
        var file = dataContext.Files.First(i => i.Id == id);

        return (Convert.FromBase64String(file.Data), file.FileName, ".pdf");
    }

    #region OrderElements



    private static List<OrderElement> OrderElements;

    public List<OrderElement> getCurrentList()
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
    public async Task AddElementInMemory(Guid productId)
    {
        var orderElement = new OrderElement();
        orderElement.Product = await dataContext.Products.Where(i => i.IdProduct == productId).Include(w => w.WareHouse).Include(i => i.Category).Include(f => f.FileImage).FirstOrDefaultAsync();
        orderElement.ProductId = orderElement.ProductId;
        orderElement.ElementsCount = 1;
        var orderElements = getCurrentList();

        orderElements.Add(orderElement);
    }

    public async Task RemoveProductInCart(OrderElement productId)
    {
        var orderElements = getCurrentList();

        orderElements.Remove(productId);
    }

    public async Task<int> CreateOrderTest(string fullAddress, 
        string CustomerName, 
        string CusomerNumber, 
        string CustomerEmail, 
        decimal totalPrice,
        Guid clientId)
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
            CustomerAddress = fullAddress,
            ClientId = clientId
        };

        order.IdOrder = Guid.NewGuid();
        var filePdf = await CreatePdfFile(order, orderElements);
        dataContext.Files.Add(filePdf);
        await dataContext.SaveChangesAsync();

        order.FileOrderId = filePdf.Id;
        foreach (var item in orderElements)
        {
            item.Product = null;
            item.OrderId = order.IdOrder;
        }
        order.OrderElements = orderElements;
        dataContext.Orders.Add(order);
        await dataContext.SaveChangesAsync();
        OrderElements = null;
        var orderInTg = new SendNewOrderMessageCommand(bot, order);
        await orderInTg.SendNewOrdersAsync();
        return order.OrderNumber;
    }

    public async Task UpdateOrderWithTg(Order order)
    {

        dataContext.Orders.Attach(order);
        await dataContext.SaveChangesAsync();
        var orderInTg = new SendNewOrderMessageCommand(bot, order);
        await orderInTg.SendPaidOrdersAsync();
    }

    public Task<FileIMG> CreatePdfFile(Order order, List<OrderElement> orderElements)
    {
        var textStyleWithFallback = TextStyle.Default
        .FontFamily(QuestPDF.Helpers.Fonts.Calibri)
        .FontSize(11);

        var generatedPdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(textStyleWithFallback);

                page.Header().Row(row =>
                {

                    row.RelativeItem().AlignLeft().Column(column =>
                    {
                        //column.Item().Width(1, Unit.Inch).Image(Path.Combine(Path.GetDirectoryName(AppContext.BaseDirectory), "Resources", "ITALZLogo.png"));
                        column.Item().Width(1, Unit.Inch).Image("C:\\Users\\leox5\\МПТ\\4 курс\\курсач\\Project\\BackOffice\\GuitarStarBackOffice\\DataBaseService\\Resources\\blue-star-logo-3.png");
                        column.Item().Text(text =>
                        {
                            text.Element().Width(10).Height(10).Image("C:\\Users\\leox5\\МПТ\\4 курс\\курсач\\Project\\BackOffice\\GuitarStarBackOffice\\DataBaseService\\Resources\\emailImg.png");
                            text.Line("    info@electro-star.ru").FontColor(Colors.Blue.Medium);
                            text.Element().Width(10).Height(10).Image("C:\\Users\\leox5\\МПТ\\4 курс\\курсач\\Project\\BackOffice\\GuitarStarBackOffice\\DataBaseService\\Resources\\phoneImg.png");
                            text.Line("    +7 495 570 00 57");

                        });
                    });


                    row.RelativeItem().AlignRight()
                    .Text(text =>
                    {
                        text.Span("Номер заказа: ").Style(textStyleWithFallback).Bold();
                        text.Line($"{order.OrderNumber}");
                        text.Span("Клиент: ").Style(textStyleWithFallback).Bold();
                        text.Line($"{order.CustomerName}");
                        text.Span("Адрес: ").Style(textStyleWithFallback).Bold();
                        text.Line($"{order.CustomerAddress}");
                        text.Span("E-mail: ").Style(textStyleWithFallback).Bold();
                        text.Line($"{order.CustomerEmail}").FontColor(Colors.Blue.Medium);
                        if (order.CustomerNumber is not null && order.CustomerNumber.Length > 0)
                        {
                            text.Span("Номер телефона: ").Style(textStyleWithFallback).Bold();
                            text.Line($"{order.CustomerNumber}");
                        }
                    });
                });


                page.Content()
                .MinimalBox()
                .Border(0)
                .Table(table =>
                {
                    IContainer DefaultCellStyle(IContainer container, string backgroundColor)
                    {
                        return container

                            .Background(backgroundColor)
                            .AlignCenter()
                            .AlignMiddle();
                    }

                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Border(0.1f).BorderColor(Colors.Black).BorderLeft(0).Element(CellStyle).Text("Фото").Style(textStyleWithFallback).Bold();

                        header.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text("Название").Style(textStyleWithFallback).Bold();
                        header.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text("Количество").Style(textStyleWithFallback).Bold();
                        header.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text("Категория").Style(textStyleWithFallback).Bold();
                        header.Cell().Border(0.1f).BorderColor(Colors.Black).BorderRight(0).Element(CellStyle).Text("Стоимость").Style(textStyleWithFallback).Bold();
                        //header.Cell().Border(0.1f).BorderColor(Colors.Black).BorderRight(0).Element(CellStyle).Text("Описание").Style(textStyleWithFallback).Bold();

                    });

                    double totalValue = 0;
                    foreach (var orderElement in orderElements)
                    {
                        if(orderElement.Product.FileImage is not null)
                        {
                            table.Cell().Border(0.1f).BorderColor(Colors.Black).BorderLeft(0).AlignLeft().Image(Convert.FromBase64String(orderElement.Product.FileImage.Data));
                        }
                        else
                        {
                            table.Cell().Border(0.1f).BorderColor(Colors.Black).BorderLeft(0).AlignLeft().Text("Фото отсутствует");
                        }
                        table.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text(orderElement.Product.ProductName);
                        table.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text(orderElement.ElementsCount);
                        table.Cell().Border(0.1f).BorderColor(Colors.Black).Element(CellStyle).Text(orderElement.Product.Category.CategoryName);
                        table.Cell().Border(0.1f).BorderColor(Colors.Black).BorderRight(0).Element(CellStyle).Text($"{Math.Round(orderElement.Product.ProductPrice * orderElement.ElementsCount, 2)} ₽");
                        //table.Cell().Border(0.1f).BorderColor(Colors.Black).Border(0.1f).BorderColor(Colors.Black).BorderRight(0).Element(CellStyle).Text(orderElement.Product.Description);

                        totalValue += orderElement.Product.ProductPrice * orderElement.ElementsCount;
                    }

                    //table.Cell().ColumnSpan(4).Border(0.1f).BorderColor(Colors.Black).BorderLeft(0).AlignLeft().Element(CellStyle).Text("Kosten van werken");
                    //table.Cell().Border(0.1f).BorderColor(Colors.Black).BorderRight(0).Element(CellStyle).Text(totalOrderValue);
                    table.Cell().ColumnSpan(4).Border(0.1f).BorderColor(Colors.Black).BorderLeft(0).BorderBottom(0).AlignLeft().Element(CellStyle).Text("Итого").Style(textStyleWithFallback).Bold().FontSize(13);
                    table.Cell().Border(0.1f).BorderColor(Colors.Black).BorderRight(0).BorderBottom(0).Element(CellStyle).Text($"{Math.Round(totalValue, 2)} ₽").Style(textStyleWithFallback).Bold().FontSize(13);
                    IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.White);

                });


            });

        }).GeneratePdf();
        var file = new FileIMG() { Data = Convert.ToBase64String(generatedPdf), FileName = $"Заказ #{order.OrderNumber}", Id = Guid.NewGuid() };
        return Task.FromResult(file);


    }

    public async Task<List<OrderElement>> GetElementsInMemory() {
        var orderElements = getCurrentList();
        return orderElements;
    }

    public async Task<List<Order>> GetOrdersForCurrentClient(Guid ClientId)
    {
        var orders = dataContext.Orders.Include(f => f.OrderElements)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.FileImage).Where(o => o.ClientId == ClientId).ToList();
        return orders;
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
        Order order = await dataContext.Orders.Include(el => el.OrderElements).ThenInclude(p => p.Product).ThenInclude(c => c.Category).Where(i => i.IdOrder == id).FirstOrDefaultAsync();

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
