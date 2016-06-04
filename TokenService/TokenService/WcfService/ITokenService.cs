using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TokenService.WcfService
{
    [ServiceContract]
    public interface ITokenService
    {
        [OperationContract]
        TokenObject createTokenForUser(int userId);
        [OperationContract]
        TokenObject findUserToken(string token);
    }
}
