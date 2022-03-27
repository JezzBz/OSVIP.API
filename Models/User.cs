using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Osvip.Api.Models
{
    
    public class User:IUserResult
    {

        
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        public string Fcs { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name ="Почта подтверждена")]
        
        public bool EmailConfirmed { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        public string SecuriryStamp { get; set; }

        [Required]  
        [ConcurrencyCheck]
        public string CouncurencyStapm { get; set; }= Guid.NewGuid().ToString();

        [Display(Name ="Номер телефона")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Роль")]
        public Roles Role { get; set; } = Roles.Student;

        [Display(Name ="Путь к картинке")]
        public string ImgPath { get; set; } = "/Source/Images/baseImages/Guest.jpeg";

        public int? Result { get; set; }
        public UserToken Token { get; set; } = new UserToken();
        public UsersTest? Test { get; set; }
        
            
    }
    public enum Roles
    {
        Admin,
        Student,
     
    }
}
