using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Osvip.Api.ServerModels
{
	public class RegisterModel
	{

        [Required]
        [Display(Name = "ФИО")]
        public string Fcs { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [MinLength(6)]
        
        public string Password { get; set; }
    }
}

