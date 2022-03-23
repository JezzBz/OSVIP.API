using System;
using Osvip.Api.Models;

namespace Osvip.Api.Interfaces
{
	public interface ITransferRequest
	{
        public string Telehone { get; set; }
        public string RequsetType { get; set; }
        public int Course { get; set; }
        public int Semester { get; set; }
       
    }
}

