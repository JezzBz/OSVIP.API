using System;
using System.Text.Json.Serialization;

namespace Osvip.Api.Models
{
    public class Test
    {
       public int Id { get; set; }
      
        public Department Department {get;set;}
        
        public int Course{ get; set;}
       
       public IEnumerable<TestQuestion> Question { get; set;}

       
        
    }
}

