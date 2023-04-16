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

}
