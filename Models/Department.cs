using System;
using System.Text.Json.Serialization;

namespace Osvip.Api.Models
{
    public class Department
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } 
    }
}

