using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenService.WcfService;

namespace TokenService.DAL
{
    public interface ITokenRepository
    {
        int Add(TokenObject token);
        bool Delete(int id);
        TokenObject Get(int id);
        List<TokenObject> GetAll();
        TokenObject Update(TokenObject token);
    }
}
