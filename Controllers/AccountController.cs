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
    /// <summary>
    /// Контроллер, отвечающий за взимодействия с аккаунтом
    /// </summary>
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Сущность для взаимодействиия с базой данных
        /// </summary>
        private readonly ApplicationContext Context;
        /// <summary>
        ///  Класс с реализацией методов взаимодействия с пользователем в базе данных
        /// </summary>
        private  readonly UserRepository userRepository;
        /// <summary>
        /// Окружение среды
        /// </summary>
        private readonly IWebHostEnvironment appEnvironment;
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(AuthOptions.LIFETIME)



        };
        /// <summary>
        /// Базовый класс случайных чисел
        /// </summary>
        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <params">Параметры полученные из Program</param>
        public AccountController(ApplicationContext _context, IWebHostEnvironment _appEnvironment)
        {
            Context = _context;
            userRepository = new UserRepository(Context);
            appEnvironment = _appEnvironment;


        }


        /// <summary>
        /// Метод регистрации пользователя
        /// </summary>
        /// <param name="model"> Необходимые данные для регистрации </param>
        /// <returns>Статус коды</returns>
        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //Проверка соответсвия всех пришедших и требуемых данных
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
                   //Создание экземпляра пользователя из пришедших данных
                    User user = CreateUser(model);
                    //Добавление пользователя в базу данных
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
                                Img = user.ImgPath,
                                
                            }
                        });
                    }
                    
                
               
              
                
                
            }

            return BadRequest();
        }

        /// <summary>
        /// Метод входа в систему
        /// </summary>
        /// <param name="model">Данные, необходимые для входа</param>
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

        /// <summary>
        /// Метод смены аватара профиля
        /// </summary>
        /// <param name="image">Картинка</param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-image")]
        [Authorize]
        public async Task<IActionResult> ChangeImage(IFormFile image)
        {
            if (image==null)
            {
                return BadRequest();
            }
            //Новое имя файла 
            var imgName = Guid.NewGuid().ToString();
            string ext = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            // путь к папке с изображенияями
            string path = "/Source/Images/UsersImages/" +imgName+ext;
                
                // сохраняем файл в папку в каталоге wwwroot
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            var email = AuthOptions.GetUserEmailByToken(HttpContext);

            User? user = userRepository.GetUserByEmail(email);
            if (user==null)
            {
                return BadRequest();
            }
            await userRepository.ChangeImage(user,path,appEnvironment.WebRootPath);
            return Ok(path);

        }
        /// <summary>
        /// Метод подтверждения почты 
        /// </summary>
        /// <param name="model">Неоходимые данные для подтверждения</param>
        /// <returns></returns>
        [HttpPost]
        [Route("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail( ConfirmEmailModel model)
        {
            //Проверка соответсвия всех пришедших и требуемых данных   
            if (ModelState.IsValid)
            {
                User? user=await userRepository.ConfirmEmail(model.userId,model.code);
                if (user != null)
                {
                    var access_token = GenerateJwtAccessToken(user);
                  
                    IUserResult userInfo = user;
                    return Ok(new
                    {
                        access_token = access_token,
                        userInfo,
                        
                    }); ;
                }
                
            }
            return BadRequest();
        }
        /// <summary>
        /// Метод выдачи информации о пользователе
        /// по токену
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UserInfo()
        {
            
            var email=AuthOptions.GetUserEmailByToken(HttpContext);
            User? user =  userRepository.GetUserByEmail(email);
            if (user!=null)
            {
                IUserResult userInfo = user;
                return Ok(new
                {
                    isNowTesting = user.Test != null,
                    userInfo
                }); 
            }
            return BadRequest();
            
        }

        
        /// <summary>
        /// Метод создания уникального токена для пользователя 
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
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
            userRepository.AddAccessToken(user, encodedJwt);
            return encodedJwt;
        }  
       /// <summary>
       /// Метод создания экземпляра пользователя из регистрационной модели
       /// </summary>
       /// <param name="model"></param>
       /// <returns>User</returns>
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
        /// <summary>
        /// Метод создания кода подтверждения почты
        /// </summary>
        /// <returns></returns>
        private int GenerateEmailConfirmToken()
        {
          
            return random.Next(1000,9999);
        }
        

     
    }
    

}
