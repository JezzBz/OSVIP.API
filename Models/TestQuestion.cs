using System;
using System.ComponentModel.DataAnnotations;

namespace Osvip.Api.Models
{
	public class TestQuestion
	{
		[Key]
		public Guid Id { get; set; }
		public string Question { get; set; }
		public IEnumerable<TestResponse> Responses { get; set; }
		
		

	}
}

			