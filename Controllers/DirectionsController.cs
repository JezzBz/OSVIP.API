using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Osvip.Api.Data;
using Osvip.Api.Models;
using Osvip.Api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Osvip.Api.Controllers
{
    [Route("api/directions")]
    public class DirectionsController : Controller
    {
        private readonly DirectionRepository repository;
        public DirectionsController(ApplicationContext context)
        {
            repository = new DirectionRepository(context);
        }

        /// <summary>
        /// Метод получения всех доступных направлений
        /// </summary>
        /// <returns>массив направлений</returns>

        [Route("all")]
        [HttpGet]
        public IEnumerable<Direction> Get()
        {
            return repository.Get();
        }
    }
}

