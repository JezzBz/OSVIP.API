using System;
using System.Text.Json.Serialization;

namespace Osvip.Api.Models
{
	public class UsersTest
	{
		public int Id { get; set; }
		public Test Test { get; set; }
		public DateTime TestStartTime { get; set; } = DateTime.UtcNow;
		[JsonIgnore]
		public User User { get; set; }
		[JsonIgnore]
		public Guid UserId { get; set; }

		
	}
}

