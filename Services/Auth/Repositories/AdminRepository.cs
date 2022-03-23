using System;
using Osvip.Api.Data;
using Osvip.Api.Models;

namespace Osvip.Api.Services.Auth.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationContext Context;
        public AdminRepository(ApplicationContext context)
        {
            Context = context;
        }

        public async Task<User?> AddUser(User user)
        {
            if (Context.Users.Any(x => x.Email == user.Email))
            {
                User existUser = Context.Users.First(x => x.Email == user.Email);
                if (!existUser.EmailConfirmed)
                {
                    return existUser;
                }
                return null;
            }
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
            return Context.Users.FirstOrDefault(x => x.Email == user.Email);
        }

        public bool RemoveUser(Guid id)
        {
            User? user = Context.Users.FirstOrDefault(x => x.UserId == id);

            if (user!=null)
            {
                try
                {
                    Context.Users.Remove(user);
                }
                catch 
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }

        public void UpdateUser(Guid id,User user)
        {
            user.UserId = id;
           
            Context.Users.Update(user);

        }
    }
}

