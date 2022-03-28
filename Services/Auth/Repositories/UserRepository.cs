
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Osvip.Api.Data;
using Osvip.Api.Models;
using Osvip.Api.ServerModels;

namespace Osvip.Api.Auth.Repositories
{
	public class UserRepository
	{
        private readonly ApplicationContext Context;

        public UserRepository(ApplicationContext context)
		{
            Context = context;
        }
        public async Task<User?> CreateAsync(User user)
        {
            
          
                if (Context.Users.Any(x=>x.Email==user.Email))
                {
                    User existUser= Context.Users.First(x => x.Email == user.Email);
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
        public async Task AddAccessToken(User user,string token)
        {
            UserToken? Token = Context.UserTokens.FirstOrDefault(x => x.user.UserId == user.UserId);

            if (Token!=null)
            {
                Token.AccessToken = token;
                user.Token = Token;
            }
            
            await Context.SaveChangesAsync();
        }
        public User Find(string email) =>  Context.Users.First(x=>x.Email==email);
        public User? SignIn(LoginModel LoginModel)
        {

            var user = Context.Users.AsNoTracking().FirstOrDefault(x => x.Email == LoginModel.Email);
            if (user!=null)
            {
                var sald = Convert.FromBase64String(user.SecuriryStamp);

                if (user.Password == AuthOptions.HashPassword(LoginModel.Password, sald))
                {
                    return user;
                }
            }
            
            return null;
        }
        public  User GetUserByEmail(string email) =>  Context.Users.Include(x=>x.Test).FirstOrDefault(x=>x.Email==email);
        public async Task ChangeImage(User user,string imgName,string envPath)
        {
            if (user.ImgPath!="Source/Images/baseImages/Guest.jpeg")
            {
                File.Delete(envPath + user.ImgPath);
            }
            
            user.ImgPath = imgName;
            await Context.SaveChangesAsync();
        }
        public bool DistinctMail(string email)
        {
            return !Context.Users.Any(x=>x.Email==email);
        }
        public async Task<bool> SaveEmailConfirmToken(User user,int emailToken)
        {
            
            user.Token.EmailConfirmToken = emailToken;
              
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<User?> ConfirmEmail(Guid userId, int emailToken)
        {
           
           
                User? user = Context.Users.FirstOrDefault(x => x.UserId == userId);
                  if (user==null)
                  {
                        return null;
                  }
                if (user.EmailConfirmed)
                {
                    return null;
                }
                UserToken token = Context.UserTokens.First(x=>x.user.UserId==userId);
                if (token.EmailConfirmToken == emailToken)
                {
                    user.EmailConfirmed = true;
                    token.EmailConfirmToken = null;
                    await Context.SaveChangesAsync();
                    return user;
                }
                return null;
            
            
        }
	}
}

