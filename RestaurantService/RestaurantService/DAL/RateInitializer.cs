using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantService.DAL
{
    public class RateInitializer : DropCreateDatabaseIfModelChanges<RateContext>
    {
        protected override void Seed(RateContext context)
        {
            base.Seed(context);
        }
    }
}