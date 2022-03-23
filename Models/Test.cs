using System;
namespace Osvip.Api.Models
{
    public class Test
    {
       public int Id { get; set; }

       public Department Department{get;set;}

       public Direction Direction { get; set;}

       public string Question { get; set;}

       public IEnumerable<TestResponse> testResponses { get; set; } 
        
    }
}

