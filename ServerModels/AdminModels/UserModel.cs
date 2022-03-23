using System;
using System.ComponentModel.DataAnnotations;
using Osvip.Api.Models;

namespace Osvip.Api.ServerModels.AdminModels
{
    public class UserModel
    {
        [Required]
        public string Fcs { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public Roles Role { get; set; } 


    }
}

