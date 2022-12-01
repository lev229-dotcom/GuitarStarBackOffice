using ClosedXML.Report;
using GuitarStarBackOffice.ServerSide.Services.ExportService.Models;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Services.ExportService;

public class UseTemplateXLSX
{
    public async Task<byte[]> Generate(Stream streamTemplate, IEnumerable<ExportOrderModel> catalogData)
    {

        var template = new XLTemplate(streamTemplate);
        template.AddVariable("Catalog", catalogData);
        template.Generate();

        MemoryStream XLSStream = new();
        template.SaveAs(XLSStream);


        return await Task.FromResult(XLSStream.ToArray());
    }

    public async Task<byte[]> GenerateEmployee(Stream streamTemplate, IEnumerable<Employee> employeeData)
    {

        var template = new XLTemplate(streamTemplate);
        template.AddVariable("Employee", employeeData);
        template.Generate();

        MemoryStream XLSStream = new();
        template.SaveAs(XLSStream);


        return await Task.FromResult(XLSStream.ToArray());
    }
}
