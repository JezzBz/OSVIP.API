using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osvip.Api.Data;
using Osvip.Api.Models;
using Osvip.Api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Osvip.Api.Controllers
{
    /// <summary>
    /// Контроллер кафедр
    /// </summary>
    [Route("api/department")]
    public class DepartmentscController : Controller
    {
        private readonly DepartmentRepository repository;
        public DepartmentscController(ApplicationContext context)
        {
            repository = new DepartmentRepository(context);
        }
        /// <summary>
        /// Метод получения всех доступных кафедр
        /// </summary>
        /// <returns>массив кафедр</returns>
        [HttpGet]
        [Route("all")]
        public IActionResult Get()
        {
            return Ok(repository.Get());
        }

        


      
    }
}

