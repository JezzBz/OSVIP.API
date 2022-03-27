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
using Osvip.Api.ServerModels;

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

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("test")]
        public async Task<IActionResult> Test([FromBody]TestRequest request )
        {
            var userEmail = AuthOptions.GetUserEmailByToken(HttpContext);
            var user =  await repository.GetTest(userEmail, request.departmentId, request.course);
            if (user.Test!=null)
            {
                return Ok(new { user.Test.Test, user.Test.TestStartTime });
            }
            
            return BadRequest();
            
        }
        [HttpPost]
        [Authorize]
        [Route("user/test")]
        public IActionResult UserTest()
        {
            var userEmail = AuthOptions.GetUserEmailByToken(HttpContext);
            User user = repository.GetUserTest(userEmail);
            if (user!=null)
            {
                return Ok(new { user.Test.Test, user.Test.TestStartTime });
            }
            return BadRequest();
        }
        [HttpPost]
        [Authorize]
        [Route("answer")]
        public async Task<IActionResult> AnswerTest([FromBody]IEnumerable<Guid> responses)
        {
            var userEmail = AuthOptions.GetUserEmailByToken(HttpContext);
            return Ok(await repository.AnsewerTest(userEmail, responses));
        }


        [HttpGet]
        [Route("test/new")]
        [Authorize(AuthenticationSchemes ="Bearer",Roles ="Admin")]
        public IActionResult Test()
        {
            return Ok();
        }
    }

}
