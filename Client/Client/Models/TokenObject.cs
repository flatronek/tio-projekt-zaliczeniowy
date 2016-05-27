using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TokenService.WcfService
{

    public class TokenObject
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string ValidityDate { get; set; }
    }
}