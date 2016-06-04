using Autofac.Integration.Wcf;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TokenService.WcfService;

namespace TokenService.DAL
{
    public class TokenInitializer : DropCreateDatabaseIfModelChanges<TokenContext>
    {
        protected override void Seed(TokenContext context)
        {
            var tokens = new List<TokenObject>();

            context.Tokens.AddRange(tokens);
            context.SaveChanges();
           
        }
    }
}