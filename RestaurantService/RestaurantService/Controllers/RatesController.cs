using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RestaurantService.DAL;
using RestaurantService.Models;
using System.Web.OData.Routing;

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
        private RateContext db = new RateContext();

        [HttpGet]
        [ODataRoute("Rate")]
        public IHttpActionResult RateRestaurant()
        {
            db.Rates.Add(new Rate() { userId=1, restaurantId=1, score=2 });
            db.SaveChanges();
            return Ok("Successful test");
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
