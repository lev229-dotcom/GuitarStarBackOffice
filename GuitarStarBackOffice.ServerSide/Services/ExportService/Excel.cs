using GuitarStarBackOffice.ServerSide.Services.ExportService.Models;

namespace GuitarStarBackOffice.ServerSide.Services.ExportService;

public class Excel
{
    private readonly UseTemplateXLSX useTemplateXLSX;

    public Excel(UseTemplateXLSX useTemplateXLSX)
    {
        this.useTemplateXLSX = useTemplateXLSX;
    }

    /// <summary>
    /// Метод для создания параметров скачивания файла
    /// </summary>
    /// <param name="streamTemplate">Содержимое шаблона</param>
    /// <param name="orderExportModels">Наименования из бд</param>
    /// <param name="filename">Имя файла</param>
    /// <returns>Возвращает сгенерированный по шаблону файл с наименованиями из бд</returns>
    public async Task<ParametersForDownloadModel> ReturnOptionsToDownLoad(Stream streamTemplate, IEnumerable<ExportOrderModel> orderExportModels, string filename = "export.xlsx")
    {

        var XLSXStream = await useTemplateXLSX.Generate(streamTemplate, orderExportModels);

        var parametrsForDownload = new ParametersForDownloadModel
        {
            Identifier = "BlazorDownloadFile",
            FileName = filename,
            Stream = XLSXStream,
        };

        return parametrsForDownload;
    }
}
