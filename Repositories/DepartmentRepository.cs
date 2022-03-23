using System;
using Osvip.Api.Data;
using Osvip.Api.Models;

namespace Osvip.Api.Repositories
{
    public class DepartmentRepository
    {
        private readonly ApplicationContext Context;
        public DepartmentRepository(ApplicationContext context)
        {
            Context = context;
        }
        public async Task<bool> AddAsync(Department department)
        {
            try
            {
                await Context.Departments.AddAsync(department);
                await Context.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }
            
            
        }
        public IEnumerable<Department> Get()
        {
            return Context.Departments.Select(x => x);
        }
    }
}

