using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using RestaurantService.DAL;
using RestaurantService.Models;
using RestaurantService.TokenService;
using System.Data.Entity;
using System;

namespace RestaurantService.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RestaurantService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Rate>("Rates");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RatesController : ODataController
    {
        private RestaurantContext db = new RestaurantContext();

        [HttpGet]
        [ODataRoute("RateCos(Melement={element})")]
        public IHttpActionResult RateCos([FromODataUri] int element)
        {

            return Ok(element*2);
        }

        [HttpGet]
        [ODataRoute("RateRestaurant(Token={token},RestaurantId={restaurantId},Score={score})")]
        public IHttpActionResult RateRestaurant([FromODataUri] string token, [FromODataUri] int restaurantId, [FromODataUri] int score)
        {
            int userId = getUserIdByToken(token);
            if (userId == -1)
                NotFound();
            Rate newRate = new Rate() { userId = userId, restaurantId = restaurantId, score = score };
            if (RateExists(newRate))
            {
                if (isRateDuplicate(newRate))
                {
                    return Ok("duplicate");
                }
                var oldRate = db.Rates.FirstOrDefault(i => i.restaurantId == restaurantId && i.userId == userId);
                if (oldRate != null)
                {
                    oldRate.score = score;
                    db.SaveChanges();
                    return Ok("Updated");
                }
            }
            db.Rates.Add(newRate);
            db.SaveChanges();
            return Ok("Added successfully");
        }


        private bool RateExists(Rate rate)
        {
            return db.Rates.Count(x => x.userId == rate.userId && x.restaurantId == rate.restaurantId) > 0;
        }

        private bool isRateDuplicate(Rate rate)
        {
            return db.Rates.Count(x => x.userId == rate.userId
                && x.restaurantId == rate.restaurantId
                && x.score == rate.score) > 0;
        }

        private void deleteRate(Rate oldRate)
        {
            var y = (from x in db.Rates where x.id == oldRate.id select x).First();
            db.Rates.Remove(y);
        }


        private int getUserIdByToken(string token)
        {
            TokenServiceClient tokenService = new TokenServiceClient();
            try
            {
                return tokenService.findUserToken(token).UserId;
            }
            catch (Exception)
            {
                return -1;
            }
        }


        // GET: odata/Rates
        [EnableQuery]
        public IQueryable<Rate> GetRates()
        {
            return db.Rates;
        }

        // GET: odata/Rates(5)
        [EnableQuery]
        public SingleResult<Rate> GetRate([FromODataUri] int key)
        {
            return SingleResult.Create(db.Rates.Where(rate => rate.id == key));
        }

        // PUT: odata/Rates(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Rate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rate rate = await db.Rates.FindAsync(key);
            if (rate == null)
            {
                return NotFound();
            }

            patch.Put(rate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rate);
        }

        // POST: odata/Rates
        public async Task<IHttpActionResult> Post(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rates.Add(rate);
            await db.SaveChangesAsync();

            return Created(rate);
        }

        // PATCH: odata/Rates(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Rate> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rate rate = await db.Rates.FindAsync(key);
            if (rate == null)
            {
                return NotFound();
            }

            patch.Patch(rate);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(rate);
        }

        // DELETE: odata/Rates(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Rate rate = await db.Rates.FindAsync(key);
            if (rate == null)
            {
                return NotFound();
            }

            db.Rates.Remove(rate);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RateExists(int key)
        {
            return db.Rates.Count(e => e.id == key) > 0;
        }
    }
}
