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
    /// <summary>
    /// Контроллер доступный только пользователям
    /// с ролью Admin
    /// </summary>
    [Route("api/admin")]
    [Authorize(AuthenticationSchemes = "Bearer",Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext context;
        public AdminController(ApplicationContext _context)
        {
            context = _context;
        }
        

        /// <summary>
        /// Метод добавления кафедры в приложение
        /// </summary>
        /// <param name="department">кафедра</param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-department")]
        public async Task<IActionResult> AddDepartment([FromBody] Department department)
        {
           
                await context.Departments.AddAsync(department);
                await context.SaveChangesAsync(); 
                return Ok();
            
            
            
        }
        /// <summary>
        /// Метод добавления направления в приложение
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-direction")]
        public async Task<IActionResult> AddDirection([FromBody]Direction direction)
        {
            
                await context.Directions.AddAsync(direction);
                await context.SaveChangesAsync();
                return Ok();
            
           
        }
        /// <summary>
        /// Метод добавления тестов в приложение 
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add-test")]
        public async Task<IActionResult> AddTest([FromBody]Test test)
        {

                test.Department = context.Departments.First(x=>x.Id==test.Department.Id);
                await context.Tests.AddAsync(test);
                await context.SaveChangesAsync();
                return Ok();
            
           
        }
    }


}

