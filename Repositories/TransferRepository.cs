using System;
using Osvip.Api.Data;
using Osvip.Api.Interfaces;
using Osvip.Api.Models;

namespace Osvip.Api.Repositories
{
    public class TransferRepository
    {
        private readonly ApplicationContext Context;
        public TransferRepository(ApplicationContext context)
        {
            Context = context;
        }
        public async Task<bool> AddAsync(Transfer transfer,User user,int departmentId,int dircetionId,string marks,string app)
        {
           
               
                transfer.User = user;
                transfer.MarksFile = marks;
                transfer.ApplicationFile = app;
                transfer.Direction = Context.Directions.First(x=>x.Id==dircetionId);
                transfer.Department = Context.Departments.First(x=>x.Id==departmentId);
                
               
                await Context.Transfers.AddAsync(transfer);
                await Context.SaveChangesAsync();
                return true;
            
            
        }
    }
}

