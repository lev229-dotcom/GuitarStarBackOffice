using Microsoft.AspNetCore.Components.Forms;

namespace GuitarStarBackOffice.ServerSide;

public static class GetFileDataRule
{
    public async static Task<string> GetData(IBrowserFile file)
    {
        var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        await using var fs = new FileStream(path, FileMode.Create);

        await file.OpenReadStream(file.Size).CopyToAsync(fs);

        var bytes = new byte[file.Size];

        fs.Position = 0;

        await fs.ReadAsync(bytes);

        fs.Close();

        File.Delete(path);

        return await Task.FromResult(Convert.ToBase64String(bytes));
    }
}
