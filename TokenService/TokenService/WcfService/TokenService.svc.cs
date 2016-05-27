using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TokenService.DAL;

namespace TokenService.WcfService
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TokenService : ITokenService
    {
        private ITokenRepository db;

        public TokenService(ITokenRepository tokenRepository)
        {
            this.db = tokenRepository;
        }

        public TokenObject createTokenForUser(int userId)
        {
            TokenObject token = db.GetAll().Find(t => t.UserId == userId);

            if (token == null)
            {
                token = new TokenObject() { UserId = userId, Token = userId.ToString(), ValidityDate = "01022017" };

                db.Add(token);
            }

            return token;
        }

        public TokenObject findUserToken(string token)
        {
            TokenObject tokenObj = db.GetAll().FirstOrDefault(t => t.Token.Equals(token));

            return tokenObj;
        }
    }
}
