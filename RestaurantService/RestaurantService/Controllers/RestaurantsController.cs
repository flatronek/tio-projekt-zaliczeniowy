using System;
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

namespace RestaurantService.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RestaurantService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Restaurant>("Restaurants");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class RestaurantsController : ODataController
    {
        private RestaurantContext db = new RestaurantContext();

        [HttpGet]
        [ODataRoute("TestFunction")]
        public IHttpActionResult TestFunction()
        {
            db.Restaurants.Add(new Restaurant() { Name = "aa2", Description = "b2", Address = "Cz2" });
            db.SaveChanges();
            return Ok("Successful test");
        }

        [HttpGet]
        [ODataRoute("MockRate")]
        public IHttpActionResult MockRate()
        {
            db.Rates.Add(new Rate() { userId = 1, restaurantId = 1, score = 2 });
            db.SaveChanges();
            return Ok("Successful test");
        }

        [HttpGet]
        [ODataRoute("Rates")]
        public IQueryable<Rate> GetRates()
        {
            return db.Rates;
        }





        // GET: odata/Restaurants
        [EnableQuery]
        public IQueryable<Restaurant> GetRestaurants()
        {
            return db.Restaurants;
        }

        // GET: odata/Restaurants(5)
        [EnableQuery]
        public SingleResult<Restaurant> GetRestaurant([FromODataUri] int key)
        {
            return SingleResult.Create(db.Restaurants.Where(restaurant => restaurant.Id == key));
        }

        // PUT: odata/Restaurants(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Restaurant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = await db.Restaurants.FindAsync(key);
            if (restaurant == null)
            {
                return NotFound();
            }

            patch.Put(restaurant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(restaurant);
        }

        // POST: odata/Restaurants
        public async Task<IHttpActionResult> Post(Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Restaurants.Add(restaurant);
            await db.SaveChangesAsync();

            return Created(restaurant);
        }

        // PATCH: odata/Restaurants(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Restaurant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = await db.Restaurants.FindAsync(key);
            if (restaurant == null)
            {
                return NotFound();
            }

            patch.Patch(restaurant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(restaurant);
        }

        // DELETE: odata/Restaurants(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Restaurant restaurant = await db.Restaurants.FindAsync(key);
            if (restaurant == null)
            {
                return NotFound();
            }

            db.Restaurants.Remove(restaurant);
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

        private bool RestaurantExists(int key)
        {
            return db.Restaurants.Count(e => e.Id == key) > 0;
        }





        private int getRateId(Rate rate)
        {
            return db.Rates.First(x => x.userId == rate.userId && x.restaurantId == rate.restaurantId).userId;
        }

        private int getRateScoreById(int id)
        {
            try
            {
                int newScore = db.Rates.First(x => x.userId == id).score;
                return newScore >= 0 ? newScore : -1;
            }
            catch
            {
                return -1;
            }
        }
    }
}
