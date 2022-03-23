using System;
namespace Osvip.Api.Models
{
    
    public class UserToken
    {
        public int Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int? EmailConfirmToken { get; set; }
        public User user { get; set; }
    }
}

