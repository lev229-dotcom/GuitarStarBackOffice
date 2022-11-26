using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace GuitarStarBackOffice.ServerSide.Pages.DiagramsDirectory;

public partial class OrderStats
{
    [Inject] private DiagramService DiagramService { get; set; }

    IEnumerable<DiagramServiceModel> orderForFirstDate;
   // IEnumerable<Order> orderForSecondDate;

    DateTime startDate { get; set; } = DateTime.Parse("7.11.2022");
    DateTime endDate { get; set; } = DateTime.Parse("15.11.2022");

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        orderForFirstDate = await DiagramService.GetOrdersByDate(startDate, endDate);
       // orderForSecondDate = await DiagramService.GetOrdersByDate(endDate);
    }

    string FormatAsRub(object value)
    {
        return ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"));
    }

    private async Task CreateStat()
    {
        orderForFirstDate = await DiagramService.GetOrdersByDate(startDate, endDate);
       // orderForSecondDate = await DiagramService.GetOrdersByDate(endDate);

    }

    string FormatAsMonth(object value)
    {
        if (value != null)
        {
            return Convert.ToDateTime(value).ToString("M");
        }

        return string.Empty;
    }
}
