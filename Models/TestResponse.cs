using System;
using System.Text.Json.Serialization;

namespace Osvip.Api.Models
{
    public class TestResponse
    {
        public Guid Id { get; set; }
        public string Response { get; set; }
     
        public int Weigth { get; set; }
        
        

    }
}

