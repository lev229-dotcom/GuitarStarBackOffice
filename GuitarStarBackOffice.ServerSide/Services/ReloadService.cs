using GuitarStarBackOffice.ServerSide.Data;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Services;

public  class ReloadService
{
    DataContext dataContext;

    public ReloadService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public  void Reload()
    {
        foreach (var entry in dataContext.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
}
