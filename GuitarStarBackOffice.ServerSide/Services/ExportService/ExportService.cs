using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.ServerSide.Services.ExportService.Models;
using GuitarStarBackOffice.Shared;
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

	public async Task<ParametersForDownloadModel> Execute(string filePath = "TemplateCatalog")
	{
		ParametersForDownloadModel result = new();

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
		if(filePath == "TemplateCatalog")
		result = await ExportCatalogBytemplate(orderExportModel, filePath);

        else if (filePath == "EmployeeTemplate")
		{
			var employeeFromDB = await dataContext.Employees.Include(e => e.Post).ToListAsync();

            result = await ExportEmployeeBytemplate(employeeFromDB, filePath);
        }


        var parametersForDownloadModel = new ParametersForDownloadModel()
		{
			Identifier = result.Identifier,
			FileName = result.FileName,
			Stream = result.Stream
		};

		return parametersForDownloadModel;

    }

    public async Task<ParametersForDownloadModel> ExportEmployeeBytemplate(IEnumerable<Employee> catalogExportModels, string filePath)
    {

        var path = Path.GetFullPath($@"C:\Users\leox5\МПТ\4 курс\курсач\Project\BackOffice\GuitarStarBackOffice\GuitarStarBackOffice.ServerSide\Services\ExportService\Resources\Excel\{filePath}.xlsx");

        using (Stream stream = File.OpenRead(path))
        {

            var options = await excel.ReturnEmployeeOptionsToDownLoad(stream, catalogExportModels);

            return options;
        }

    }

    public async Task<ParametersForDownloadModel> ExportCatalogBytemplate(IEnumerable<ExportOrderModel> catalogExportModels, string filePath)
    {

        var path = Path.GetFullPath($@"C:\Users\leox5\МПТ\4 курс\курсач\Project\BackOffice\GuitarStarBackOffice\GuitarStarBackOffice.ServerSide\Services\ExportService\Resources\Excel\{filePath}.xlsx");

        using (Stream stream = File.OpenRead(path))
        {

            var options = await excel.ReturnOptionsToDownLoad(stream, catalogExportModels);

            return options;
        }

    }
}
