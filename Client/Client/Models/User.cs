using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class DbUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}