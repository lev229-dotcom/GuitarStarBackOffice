using ClosedXML.Report;
using GuitarStarBackOffice.ServerSide.Services.ExportService.Models;

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
}
