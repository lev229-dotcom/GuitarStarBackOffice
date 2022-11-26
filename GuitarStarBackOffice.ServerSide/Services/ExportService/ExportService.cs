using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services.ExportService.Models;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Extensions;

namespace GuitarStarBackOffice.ServerSide.Services.ExportService;

public class ExportService
{
    DataContext dataContext;
    private readonly Excel excel;

	public ExportService(DataContext dataContext, Excel excel)
	{
		this.dataContext = dataContext;
		this.excel = excel;
	}

	public async Task<ParametersForDownloadModel> Execute()
	{

		var orders = await dataContext.Orders.Include(e => e.Employee).ThenInclude(d => d.Post).ToListAsync();

		var orderExportModel = from element in orders
							   select new ExportOrderModel
							   {
								   OrderNumber = element.OrderNumber,
								   OrderDate = element.OrderDate,
								   TotalOrderAmount = element.TotalOrderAmount,
								   orderStatus = element.orderStatus.ToDescriptionString(),
								   paymentStatus = element.payementStatus.ToDescriptionString(),
								   EmployeeFullName = $"{element.Employee.Surname} {element.Employee.Name} {element.Employee.Patronymic}"
							   };

		var result = await ExportCatalogBytemplate(orderExportModel);

		var parametersForDownloadModel = new ParametersForDownloadModel()
		{
			Identifier = result.Identifier,
			FileName = result.FileName,
			Stream = result.Stream
		};

		return parametersForDownloadModel;

    }

    public async Task<ParametersForDownloadModel> ExportCatalogBytemplate(IEnumerable<ExportOrderModel> catalogExportModels)
    {

        var path = Path.GetFullPath(@"C:\Users\leox5\МПТ\4 курс\курсач\Project\BackOffice\GuitarStarBackOffice\GuitarStarBackOffice.ServerSide\Services\ExportService\Resources\Excel\TemplateCatalog.xlsx");

        using (Stream stream = File.OpenRead(path))
        {

            var options = await excel.ReturnOptionsToDownLoad(stream, catalogExportModels);

            return options;
        }

    }
}
