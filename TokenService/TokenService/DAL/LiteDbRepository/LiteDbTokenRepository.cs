using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenService.WcfService;

namespace TokenService.DAL.LiteDbRepository
{
    public class LiteDbTokenRepository : ITokenRepository
    {
        private readonly string _databaseConnection = @"C:\tmp\tokens";
        private readonly string _collectionName = "tokens";

        public int Add(TokenObject token)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<TokenObject>(_collectionName);

                if (repository.FindById(token.UserId) != null)
                    repository.Update(token);
                else
                    repository.Insert(token);

                return token.UserId;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<TokenObject>(_collectionName);

                return repository.Delete(id);
            }
        }

        public TokenObject Get(int id)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<TokenObject>(_collectionName);

                return repository.FindById(id);
            }
        }

        public List<TokenObject> GetAll()
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                return db.GetCollection<TokenObject>(_collectionName).FindAll().ToList();
            }
        }

        public TokenObject Update(TokenObject token)
        {
            using (var db = new LiteDatabase(_databaseConnection))
            {
                var repository = db.GetCollection<TokenObject>(_collectionName);

                if (repository.Update(token))
                    return token;
                else
                    return null;
            }
        }
    }
}