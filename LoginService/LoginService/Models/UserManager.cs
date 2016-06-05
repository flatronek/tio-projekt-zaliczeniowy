using LoginService.DAL;
using LoginService.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginService.Models
{
    public class UserManager
    {
        private UserContext db;
        private TokenServiceClient tokenService;

        public UserManager()
        {
            db = new UserContext();
            tokenService = new TokenServiceClient();
        }

        public DbUser GetUserName(int id)
        {
            return db.Users.Where(x => x.Id == id).ToList().First();
        }

        public List<DbUser> ListUsers()
        {
            return db.Users.ToList();
        }

        public Boolean RegisterUser(RequestUser user)
        {
            if (db.Users.Count(u => u.Login.Equals(user.Login)) > 0)
            {
                return false;
            }

            DbUser newUser = new DbUser(db.Users.Count() + 1 , user);

            db.Users.Add(newUser);
            db.SaveChanges();

            return true;
        }

        public TokenResponse LoginUser(UserCredentials credentials)
        {
            TokenResponse response = null;

            if (db.Users.Count(u => u.Login.Equals(credentials.Login)) > 0)
            {
                DbUser dbUser = db.Users.First(u => u.Login.Equals(credentials.Login));
            
                if (dbUser.Password.Equals(credentials.Password))
                {
                    TokenObject tokenObj = tokenService.createTokenForUser(dbUser.Id);

                    response = new TokenResponse() { Token = tokenObj.Token, ValidationDate = tokenObj.ValidityDate };
                }
            }

            return response;
        }
    }
}