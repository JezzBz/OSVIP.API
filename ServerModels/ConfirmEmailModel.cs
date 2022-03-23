using System;
namespace Osvip.Api.ServerModels
{
    public class ConfirmEmailModel
    {
        public Guid userId { get; set; }
        public int code { get; set; }
    }
}

