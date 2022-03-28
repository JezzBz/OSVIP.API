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
       /// <summary>
       /// Выборка всех кафедр из базы данных
       /// </summary>
       /// <returns></returns>
        public IEnumerable<Department> Get()
        {
            return Context.Departments.Select(x => x);
        }
    }
}

