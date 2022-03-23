using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Osvip.Api.Auth.Repositories;
using Osvip.Api.Data;
using Osvip.Api.Interfaces;
using Osvip.Api.Models;
using Osvip.Api.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Osvip.Api.Controllers
{
    [Route("api/transfer")]
    public class TransferController : Controller
    {
        private readonly TransferRepository reposiroty;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly UserRepository userRepository;
        public TransferController(ApplicationContext context, IWebHostEnvironment _appEnvironment)
        {
            reposiroty = new TransferRepository(context);
            userRepository = new UserRepository(context);
            appEnvironment = _appEnvironment;
        }


        [HttpPost]
        [Route("new")]
        [Authorize]
        public async Task<IActionResult> AddNew(Transfer transfer, int departmentId, int directionId, IFormFile marks, IFormFile application)
        {
            if (!(
                transfer.RequestType == "Восстановление внутри МАИ" ||
                transfer.RequestType == "Перевод внутри МАИ" ||
                transfer.RequestType == "Перевод из другого ВУЗа") ||
                transfer.Semester == 0||
                transfer.Course==0)
            {
                return BadRequest();
            }
            if (marks == null||application==null)
            {
                return BadRequest();
            }
            var MarksName = Guid.NewGuid().ToString();
            var ApplicationName = Guid.NewGuid().ToString();
            string extMarks = marks.FileName.Substring(marks.FileName.LastIndexOf('.'));
            string extApplication= application.FileName.Substring(application.FileName.LastIndexOf('.'));

            
            string pathMarks = "/Source/Documents/" + MarksName + extMarks;
            string pathApplication= "/Source/Documents/" + ApplicationName + extApplication;

            // сохраняем файл в папку Files в каталоге wwwroot
            using (var fileStream = new FileStream(appEnvironment.WebRootPath + pathMarks, FileMode.Create))
            {
                await marks.CopyToAsync(fileStream);
            }
            using (var fileStream = new FileStream(appEnvironment.WebRootPath + pathApplication, FileMode.Create))
            {
                await application.CopyToAsync(fileStream);
            }

            string Token = HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1];

            IEnumerable<Claim> Claims = AuthOptions.ReadJwtAccessToken(Token);

            User? user = userRepository.GetUserByEmailAsync(
    email: Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value);

            if (user != null)
            {
                if (await reposiroty.AddAsync(transfer, user, departmentId, directionId,pathMarks,pathApplication))
                {
                    return Ok();
                }
            }
               
            return BadRequest();
        }
        
    }
}

