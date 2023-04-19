using DataBaseService.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseService.Services;

public class WishlistElementsService
{
    DataContext dataContext;

    public WishlistElementsService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<List<WishlistElements>> GetWishlistElementsForCurrentClient(Guid ClientId)
    {
        var wishListElements = dataContext.WishlistElements
            .Include(p => p.Product)
            .ThenInclude(f => f.FileImage)
            .Where(p => p.ClientId == ClientId).ToList();
        return wishListElements;
    }

    public async Task<bool> AddWishElement(WishlistElements wishlistElement)
    {
        var isOldNote = await dataContext.WishlistElements.Where(cl => cl.ClientId == wishlistElement.ClientId && cl.ProductId == wishlistElement.ProductId).FirstOrDefaultAsync();
        if(isOldNote != null)
            return false;
        dataContext.WishlistElements.Add(wishlistElement);
        await dataContext.SaveChangesAsync();
        return true;
    }
    public async Task RemoveWishElement(Guid wishlistElementId)
    {
        var wishlistElement = await dataContext.WishlistElements.FirstAsync(w => w.IdWishlistElement == wishlistElementId);
        dataContext.WishlistElements.Remove(wishlistElement);

        await dataContext.SaveChangesAsync();
    }
}
