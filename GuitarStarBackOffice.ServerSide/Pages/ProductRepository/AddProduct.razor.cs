using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.ProductRepository;

public partial class AddProduct
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private ProductService ProductService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Product newProduct = new();

    IEnumerable<Category> categories;
    IEnumerable<WareHouse> wareHouses;

    EditContext editContext;

    private bool IsActive = true;

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();

        editContext = new EditContext(newProduct);
        editContext.OnFieldChanged += FieldChanged;

    }
    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
    }
    public async Task Get()
    {
        categories = await ProductService.GetCategories();
        wareHouses = await ProductService.GetWareHouses();
    }

    private async Task HandleAdd()
    {
        try
        {
            await ProductService.AddProduct(newProduct);
            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
        }
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);

    }
}
