using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osvip.Api.Data;
using Osvip.Api.Models;
using Osvip.Api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Osvip.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestsController : Controller
    {
        private readonly TestRepository repository;
        public TestsController(ApplicationContext context)
        {
            repository = new TestRepository(context);
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get(int DepartmentId,int DirectionId)
        {
            return Ok(repository.GetTests(DepartmentId,DirectionId));
        }

        [HttpGet]
        [Route("test")]
        [Authorize(AuthenticationSchemes ="Bearer",Roles ="Admin")]
        public IActionResult Test()
        {
            return Ok();
        }
    }

}
//http://localhost:7239/api/Tests
//eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidmViZ2hvc3RAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiU3R1ZGVudCIsIm5iZiI6MTY0NzczNTAyNywiZXhwIjoxNjQ3ODIxNDI3LCJpc3MiOiJPc3ZpcEFwaSIsImF1ZCI6Ik9TVklQQ2xJRU5UIn0.48YdUQNQmGMMxfvTD7iu6_cbp4DQ0ojSYCq4TSrRomU