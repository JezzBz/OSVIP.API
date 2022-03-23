using System;
using Osvip.Api.Interfaces;

namespace Osvip.Api.Models
{
    public class Transfer
    {
        public Guid  Id { get; set; }
        public User User { get; set;}
        public string RequestType { get; set; }
        public int Course { get; set; }
        public int Semester { get; set; }
        public string Telephone { get; set; }
        public Direction Direction { get; set; }
        public Department Department { get; set; }
        public string MarksFile { get; set; }
        public string ApplicationFile { get; set; }
    }
}

