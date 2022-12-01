using BlazorDownloadFile;
using GuitarStarBackOffice.ServerSide.Data;
using Microsoft.Data.SqlClient;

namespace GuitarStarBackOffice.ServerSide.Services.BackUpService
{
    public class BackupService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _backupFolderFullPath = AppDomain.CurrentDomain.BaseDirectory + "Backup\\backup.bak";
        private readonly string _directory = AppDomain.CurrentDomain.BaseDirectory + "Backup";
        private readonly BlazorDownloadFile.IBlazorDownloadFileService blazorDownloadFile;
        private readonly DataContext storeDbContext;



        public BackupService(IConfiguration configuration, IBlazorDownloadFileService blazorDownloadFile, DataContext storeDbContext)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }
            this.blazorDownloadFile = blazorDownloadFile;
            this.storeDbContext = storeDbContext;
        }


        public async Task BackupDatabase()
        {
            if (!File.Exists(_backupFolderFullPath))
            {
                File.Create(_backupFolderFullPath);
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = String.Format("BACKUP DATABASE [GuitarStarDb] TO DISK='{0}' WITH INIT, FORMAT", _backupFolderFullPath);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                using (FileStream reader = new FileStream(_backupFolderFullPath, FileMode.Open))
                {
                    await blazorDownloadFile.DownloadFile("BackUpGuitarStar_" + DateTime.Now.ToShortDateString() + ".bak", reader, "application/ostet-stream");
                }
            }
        }

        public async Task RestoreBatabase(byte[] stream)
        {
            if (!File.Exists(_backupFolderFullPath))
            {
                File.Create(_backupFolderFullPath);
            }
            using (MemoryStream mem = new MemoryStream(stream))
            {
                await File.WriteAllBytesAsync(_backupFolderFullPath, mem.ToArray());
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = String.Format("USE MASTER RESTORE DATABASE [GuitarStarDb] FROM DISK='{0}' WITH REPLACE", _backupFolderFullPath);
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            var entitiesList = storeDbContext.ChangeTracker.Entries().ToList();
            foreach (var entity in entitiesList)
            {
                entity.Reload();
            }
        }
    }
}
