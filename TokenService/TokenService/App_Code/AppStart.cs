using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenService.WcfService;

namespace TokenService.App_Code
{
    public static class AppStart
    {
        public static void AppInitialize()
        {
            TokenService.WcfService.TokenService.InitAutofac();
        }
    }
}