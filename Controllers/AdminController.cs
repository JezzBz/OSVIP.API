using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osvip.Api.Data;
using Osvip.Api.Models;
using Osvip.Api.ServerModels.AdminModels;
using Osvip.Api.Services.Auth.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Osvip.Api.Controllers
{
    [Route("api/admin")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AdminRepository repository;
        public AdminController(ApplicationContext context)
        {
            repository = new AdminRepository(context);
        }

        [HttpPost]
        [Route("user-add")]
        public async Task<IActionResult> AddUser(UserModel userModel)
        {
            User? user = CreateUserFromModel(userModel);
            if (user!=null)
            {
                await repository.AddUser(user);
                return Ok();
            }
            return BadRequest();
           
        }
        [HttpDelete]
        [Route("user-remove")]
        public IActionResult RemoveUser(Guid userId)
        {
            if (repository.RemoveUser(userId))
            {
                return Ok();
            }

            return BadRequest();
        }
        [HttpPut]
        [Route("user-update")]
        public IActionResult UpdateUser(Guid userId, UserModel userModel)
        {
            User? user = CreateUserFromModel(userModel);
            if (user==null)
            {
                return BadRequest();
            }
            repository.UpdateUser(userId, user);
            return Ok();
            
        }


        private static User? CreateUserFromModel(UserModel userModel)
        {
            if (!AuthOptions.ValidateEmail(userModel.Email) || !AuthOptions.ValidetePassword(userModel.Password))
            {
                return null;
            }
            User user = new User();
            user.Email = userModel.Email;
            user.Fcs = userModel.Fcs;
            byte[] salt = AuthOptions.GenerateSault();
            user.Password = AuthOptions.HashPassword(password: userModel.Password, salt: salt);
            user.Role = userModel.Role;
            user.EmailConfirmed = true;
            return user;
        }
    }


}

