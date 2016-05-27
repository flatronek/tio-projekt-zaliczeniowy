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
        private TokenContext db = new TokenContext();

        public TokenObject createTokenForUser(int userId)
        {
            TokenObject token = null;
            try
            {
                IQueryable<TokenObject> tokens = db.Tokens.Where(x => x.UserId == userId);
                if (tokens.Any())
                    token = tokens.First();
                
            }
            catch (Exception e)
            {
                e.GetBaseException();
            }

            if (token == null)
            {
                token = new TokenObject() { UserId = userId, Token = userId.ToString(), ValidityDate = "01022017" };

                db.Tokens.Add(token);
                db.SaveChanges();
            }

            return token;
        }

        public TokenObject findUserToken(string token)
        {
            IQueryable<TokenObject> seq = db.Tokens.Where(t => t.Token.Equals(token));

            if (seq.Any())
            {
                return seq.First();
            }

            return null;
        }
    }
}
