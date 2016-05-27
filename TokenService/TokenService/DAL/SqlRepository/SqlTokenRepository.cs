using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TokenService.WcfService;

namespace TokenService.DAL.SqlRepository
{
    public class SqlTokenRepository : ITokenRepository
    {
        private TokenContext db = new TokenContext();

        public int Add(TokenObject token)
        {
            db.Tokens.Add(token);
            db.SaveChanges();

            return token.UserId;
        }

        public bool Delete(int id)
        {
            TokenObject token = db.Tokens.Find(id);
            if (token == null)
            {
                return false;
            }

            db.Tokens.Remove(token);
            db.SaveChanges();

            return true;
        }

        public TokenObject Get(int id)
        {
            TokenObject token = db.Tokens.Find(id);

            return token;
        }

        public List<TokenObject> GetAll()
        {
            return db.Tokens.ToList();
        }

        public TokenObject Update(TokenObject token)
        {
            db.Entry(token).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return token;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(token.UserId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool TokenExists(int id)
        {
            return db.Tokens.Count(e => e.UserId == id) > 0;
        }
    }
}