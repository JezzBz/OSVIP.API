using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Osvip.Api.Auth.Repositories;
using Osvip.Api.Data;
using Osvip.Api.Models;

using Osvip.Api.ServerModels;
using Osvip.Api.Services;

namespace Osvip.Api.Controllers
{

    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext Context;

        private  readonly UserRepository userRepository;

        private readonly IWebHostEnvironment appEnvironment;

        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(AuthOptions.LIFETIME)



        };

        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());



        public AccountController(ApplicationContext _context, IWebHostEnvironment _appEnvironment)
        {
            Context = _context;
            userRepository = new UserRepository(Context);
            appEnvironment = _appEnvironment;


        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
           
            if (ModelState.IsValid)
            {
               
                    if (!AuthOptions.ValidetePassword(model.Password))
                    {
                        return BadRequest("Bad password!");
                    }
                    if (!AuthOptions.ValidateEmail(model.Email))
                    {
                        return BadRequest("Bad E-mail adress!");
                    }
                   
                    User user = CreateUser(model);
                    User? newUser = await userRepository.CreateAsync(user);
                    if (newUser != null)
                    {
                        EmailService emailService = new EmailService();
                        var code = GenerateEmailConfirmToken();
                        await emailService.SendEmailAsync(newUser.Email, "Подтверждение почты", string.Format("Ваш код активации аккаунта:{0}", code));
                        await userRepository.SaveEmailConfirmToken(newUser, code);

                        return Ok(new
                        {
                            UserInfo = new
                            {
                                Fcs = newUser.Fcs,
                                Id = newUser.UserId,
                                email = newUser.Email,
                                Img = user.ImgPath
                            }
                        });
                    }
                    
                
               
              
                
                
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
               
                    User? user = userRepository.SignIn(model);
                    if (user != null&&user.EmailConfirmed)
                    {
                        string access_token = GenerateJwtAccessToken(user);
                        //!!!
                        Response.Cookies.Append("access_token", access_token, cookieOptions);

                    IUserResult userInfo = user;
                    return Ok(new
                    {
                        access_token=access_token,
                        userInfo
                    });
                }
                 
                
                
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("change-image")]
        [Authorize]
        public async Task<IActionResult> ChangeImage(IFormFile image)
        {
            if (image==null)
            {
                return BadRequest();
            }
            var imgName = Guid.NewGuid().ToString();
            string ext = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            // путь к папке Files
            string path = "/Source/Images/UsersImages/" +imgName+ext;
                
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            string Token = HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1];

            IEnumerable<Claim> Claims = AuthOptions.ReadJwtAccessToken(Token);

            User? user = userRepository.GetUserByEmailAsync(
                email: Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value);
            if (user==null)
            {
                return BadRequest();
            }
            await userRepository.ChangeImage(user,path,appEnvironment.WebRootPath);
            return Ok(path);

        }
        
        [HttpPost]
        [Route("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail( ConfirmEmailModel model)
        {
            
            if (ModelState.IsValid)
            {
                User? user=await userRepository.ConfirmEmail(model.userId,model.code);
                if (user != null)
                {
                    var access_token = GenerateJwtAccessToken(user);
                    Response.Cookies.Append("access_token", access_token, cookieOptions);
                    IUserResult userInfo = user;
                    return Ok(new
                    {
                        access_token=access_token,
                        userInfo
                    });
                }
                
            }
            return BadRequest();
        }
        
        [HttpGet]
        [Route("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserInfo()
        {

            string Token =HttpContext.Request.Headers.Authorization.ToString().Split(" ")[1];

            IEnumerable<Claim> Claims = AuthOptions.ReadJwtAccessToken(Token);

            User? user = userRepository.GetUserByEmailAsync(
                email: Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value);

            if (user!=null)
            {
                IUserResult userInfo = user;
                return Ok(new
                {
                    
                    userInfo
                }); 
            }
            return BadRequest();
            
        }

        
        private string GenerateJwtAccessToken(User user)
        {
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: AuthOptions.GetIdentity(user).Claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(AuthOptions.LIFETIME),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
               
                        );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            userRepository.AddAccessToken(user,encodedJwt);
            return encodedJwt;
        }
        
       
        private User CreateUser(RegisterModel model)
        {
            var user = new User();
            user.Email = model.Email;
            user.Fcs = model.Fcs;
            byte[] salt = AuthOptions.GenerateSault();
            user.Password = AuthOptions.HashPassword(password: model.Password, salt: salt);
            user.SecuriryStamp = Convert.ToBase64String(salt);
            return user;
        }
        
        private int GenerateEmailConfirmToken()
        {
          
            return random.Next(1000,9999);
        }
        

     
    }
    

}
