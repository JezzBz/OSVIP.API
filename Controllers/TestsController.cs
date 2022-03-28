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
        /// <summary>
        ///  Получения теста пользователем
        /// </summary>
        /// <param name="request">модель данных о тесте</param>
        /// <returns>тест и время начала</returns>
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

        /// <summary>
        /// Выдача присвоенного теста авторизованного пользователя 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Метод присвоения результатов теста по ответам
        /// </summary>
        /// <param name="responses">Экземпляры ответов</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("answer")]
        public async Task<IActionResult> AnswerTest([FromBody]IEnumerable<Guid> responses)
        {
            var userEmail = AuthOptions.GetUserEmailByToken(HttpContext);
            return Ok(await repository.AnsewerTest(userEmail, responses));
        }


        
    }

}
