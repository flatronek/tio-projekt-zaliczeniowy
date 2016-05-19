using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string ValidationDate { get; set; }
    }
}