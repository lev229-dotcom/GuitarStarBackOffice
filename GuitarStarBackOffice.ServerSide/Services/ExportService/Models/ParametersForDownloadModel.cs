namespace GuitarStarBackOffice.ServerSide.Services.ExportService.Models;

public class ParametersForDownloadModel
{
    public string Identifier { get; set; }

    public string FileName { get; set; }

    public byte[] Stream { get; set; }
}
