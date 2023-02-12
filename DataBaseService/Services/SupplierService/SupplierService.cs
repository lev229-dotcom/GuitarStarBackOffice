using DataBaseService.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace DataBaseService.Services.SupplierService;

public class SupplierService
{
    DataContext dataContext;
    public List<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public SupplierService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<IEnumerable<Supplier>> GetSuppliers()
    {
        var suppliers = dataContext.Supplier.ToList();

        return suppliers;
    }

    public async Task<Supplier> GetSupplierById(Guid id)
    {
        Supplier supplier = await dataContext.Supplier.Where(i => i.IdSupplier == id).FirstOrDefaultAsync();

        return await Task.FromResult(supplier);
    }

    public async Task UpdateSupplier(Supplier supplier)
    {
        dataContext.Supplier.Attach(supplier);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddSupplier(Supplier supplier)
    {
        dataContext.Supplier.Add(supplier);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteSupplier(Supplier supplier)
    {
        dataContext.Supplier.Remove(supplier);
        await dataContext.SaveChangesAsync();
    }
}
