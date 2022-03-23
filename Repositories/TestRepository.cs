using System;
using Osvip.Api.Data;
using Osvip.Api.Models;

namespace Osvip.Api.Repositories
{
    public class TestRepository
    {
        private readonly ApplicationContext Context;
        public TestRepository(ApplicationContext context)
        {
            Context = context;
        }
        public async Task<bool>  AddAsync(Test test,IEnumerable<TestResponse> responses)
        {
            try
            {
                await Context.Tests.AddAsync(test);
                await Context.TestResponses.AddRangeAsync(responses);
                await Context.SaveChangesAsync();
                  
                return true;
            }
            catch 
            {
                return false;
            }
           

        }

        public IEnumerable<Test> GetTests(int depId,int dirId)
        {
            return Context.Tests.Where(x=>x.Department.Id==depId&&x.Direction.Id==dirId);
        }
    }
}

