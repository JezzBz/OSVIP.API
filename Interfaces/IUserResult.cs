using System;
using Osvip.Api.Models;

namespace Osvip.Api
{
	public interface IUserResult
	{
        public Guid UserId { get; set; }
       
        public string Fcs { get; set; }

        public string Email { get; set; }

        public Roles Role { get; set; }

        public int? Result { get; set; }
        public UsersTest Test { get; set; }
        public string ImgPath { get; set; }
    }
}

