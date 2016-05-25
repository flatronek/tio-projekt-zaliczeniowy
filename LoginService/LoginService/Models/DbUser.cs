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

        public DbUser()
        {

        }

        public DbUser(int id, RequestUser requestUser)
        {
            this.Id = id;
            this.Name = requestUser.Name;
            this.Login = requestUser.Login;
            this.Password = requestUser.Password;
        }
    }
}