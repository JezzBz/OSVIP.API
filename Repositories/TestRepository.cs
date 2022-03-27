using System;
using Microsoft.EntityFrameworkCore;
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
      

       
        public async  Task<User?> GetTest(string userEmail,int department,int course)
        {
            
            var user =  Context.Users
                .Include(x=>x.Test)
                .ThenInclude(x=>x.Test)
                .ThenInclude(x=>x.Question)
                .ThenInclude(x=>x.Responses)
                .First(x=>x.Email==userEmail&&x.EmailConfirmed);
                
                if (user.Test!=null)
                {
                    return user;
                }

            var Test=  Context.Tests
                .Include(x => x.Question)
                .ThenInclude(x => x.Responses)
                .FirstOrDefault(x => x.Course == course && x.Department.Id == department);
           
                user.Test = new UsersTest
                {
                    Test = Test
                };


            Context.Users.Local.Add(user);
            Context.SaveChanges();

            return Context.Users.Include(x => x.Test)
                .ThenInclude(x => x.Test)
                .ThenInclude(x => x.Question)
                .ThenInclude(x => x.Responses)
                .FirstOrDefault(x => x.Email == userEmail && x.EmailConfirmed);
            ;




        }

        public async Task<int?> AnsewerTest(string userEmail, IEnumerable<Guid> responses)
        {
            User user = await Context.Users.FirstAsync(x=>x.Email==userEmail);

            var Responses = Context.TestResponses.Where(x => responses.Contains(x.Id)).Select(x=>x);
            user.Result = Responses.Sum(x => x.Weigth);
            await Context.SaveChangesAsync();
            return user.Result;
        }

        public User? GetUserTest(string userEmail)
        {  
           
            User? user = Context.Users.Include(x => x.Test)
                .ThenInclude(x => x.Test)
                .ThenInclude(x => x.Question)
                .ThenInclude(x => x.Responses)
                .FirstOrDefault(x => x.Email == userEmail && x.EmailConfirmed);


            return user;
        }
    }
}

