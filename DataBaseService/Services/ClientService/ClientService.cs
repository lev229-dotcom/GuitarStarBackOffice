using DataBaseService.Data;
using DataBaseService.Services.EmployeeService.Models;
using DataBaseService.Services.Hash;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseService.Services.ClientService;

public class ClientService
{
    DataContext dataContext;

	public ClientService(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}

    public async Task<Client> Authorize(AuthenticateInputModel authenticateInputModel)
    {
        var employee = dataContext.Clients
            .Where(i => i.ClientEmail == authenticateInputModel.Email &&
            i.ClientPassword == HashHelper.GetHashString(authenticateInputModel.Password))
            .FirstOrDefault();

        return await Task.FromResult(employee);
    }

    public async Task<Client> GetEmployeeById(Guid id)
    {
        var client = await dataContext.Clients
            .Include(d => d.Orders)
            .Include(w => w.WishlistElements)
            .Where(i => i.IdClient == id)
            .FirstOrDefaultAsync();

        return await Task.FromResult(client);
    }

    public async Task<bool> UpdateClient(Client client, bool IsNewPassword = false)
    {
        try
        {
            if (IsNewPassword)
                client.ClientPassword = HashHelper.GetHashString(client.ClientPassword);
            else
                client.ClientPassword = client.ClientPassword;
            dataContext.Clients.Attach(client);
            dataContext.SaveChanges();

            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }


    public async Task<bool> Register(Client client)
    {
        try
        {
            client.ClientPassword = HashHelper.GetHashString(client.ClientPassword);
            dataContext.Clients.Add(client);
            await dataContext.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
        
    }

}
