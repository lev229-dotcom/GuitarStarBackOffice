using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace GuitarStarBackOffice.ServerSide.Pages.DiagramsDirectory;

public partial class OrderStats
{
    [Inject] private DiagramService DiagramService { get; set; }

    IEnumerable<Order> orderForFirstDate;
    IEnumerable<Order> orderForSecondDate;

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        orderForFirstDate = await DiagramService.GetOrdersByDate(DateTime.Parse("16.11.2022"));
        orderForSecondDate = await DiagramService.GetOrdersByDate(DateTime.Parse("15.11.2022"));
    }

    string FormatAsRub(object value)
    {
        return ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"));
    }

}
