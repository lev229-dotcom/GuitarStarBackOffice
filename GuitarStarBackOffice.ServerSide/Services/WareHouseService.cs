using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Services;

public class WareHouseService
{
    DataContext dataContext;

    public List<WareHouse> WareHouses { get; set; } = new List<WareHouse>();
    public List<Shipment> Shipments { get; set; } = new ();

    public WareHouseService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<IEnumerable<WareHouse>> GetWareHouses()
    {
        var wareHouses = dataContext.WareHouses.ToList();

        return wareHouses;
    }

    #region Shipmnets

    public async Task<IEnumerable<Supplier>> GetSuppliers()
    {
        var suppliers = dataContext.Supplier.ToList();

        return suppliers;
    }

    public async Task<IEnumerable<Shipment>> GetShipments()
    {
        var suppliers = await dataContext.Shipments.Include(w => w.Warehouse).Include(s => s.Supplier).ToListAsync();

        return suppliers;
    }

    public async Task<IEnumerable<Shipment>> GetShipmentsByWareHouseId(Guid WareHouseId)
    {
        var suppliers = await dataContext.Shipments.Include(w => w.Warehouse).Include(s => s.Supplier).Where(i => i.WareHouseId == WareHouseId).ToListAsync();

        return suppliers;
    }

    public async Task<Shipment> GetShipmentByTwoId(Guid shipmentId, Guid wareHouseId)
    {
        var shipment = await dataContext.Shipments.Where(i => i.IdShipment == shipmentId && i.WareHouseId == wareHouseId).FirstOrDefaultAsync();

        return await Task.FromResult(shipment);
    }

    public async Task AddShipmentToCurrentWareHouse(Shipment shipment)
    {
        dataContext.Shipments.Add(shipment);
        await dataContext.SaveChangesAsync();
    }

    public async Task UpdateShipment(Shipment shipment)
    {
        dataContext.Shipments.Attach(shipment);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteShipmnet(Shipment shipment)
    {
        dataContext.Shipments.Remove(shipment);
        await dataContext.SaveChangesAsync();
    }

    #endregion Shipments

    #region Products

    public async Task<IEnumerable<Product>> GetProductsByWareHouseId(Guid WareHouseId)
    {
        var suppliers = await dataContext.Products.Include(w => w.WareHouse).Include(s => s.Category).Where(i => i.WareHouseId == WareHouseId).ToListAsync();

        return suppliers;
    }

    public async Task AddProductToCurrentWareHouse(Product product)
    {
        dataContext.Products.Add(product);
        await dataContext.SaveChangesAsync();
    }

    #endregion Products
    public async Task<WareHouse> GetWareHouseById(Guid id)
    {
        var wareHouse = await dataContext.WareHouses.Where(i => i.IdEWareHouse == id).FirstOrDefaultAsync();

        return await Task.FromResult(wareHouse);
    }


    public async Task UpdateWareHouse(WareHouse wareHouse)
    {
        dataContext.WareHouses.Attach(wareHouse);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddWareHouse(WareHouse wareHouse)
    {
        dataContext.WareHouses.Add(wareHouse);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeletewareHouse(WareHouse wareHouse)
    {
        dataContext.WareHouses.Remove(wareHouse);
        await dataContext.SaveChangesAsync();
    }

}
