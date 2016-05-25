using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using RestaurantService.DAL;
using RestaurantService.Models;

namespace RestaurantService.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using RestaurantService.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DbRestaurant>("DbRestaurants");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DbRestaurantsController : ODataController
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: odata/DbRestaurants
        [EnableQuery]
        public IQueryable<DbRestaurant> GetDbRestaurants()
        {
            return db.DbRestaurants;
        }

        // GET: odata/DbRestaurants(5)
        [EnableQuery]
        public SingleResult<DbRestaurant> GetDbRestaurant([FromODataUri] int key)
        {
            return SingleResult.Create(db.DbRestaurants.Where(dbRestaurant => dbRestaurant.Id == key));
        }

        // PUT: odata/DbRestaurants(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<DbRestaurant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DbRestaurant dbRestaurant = db.DbRestaurants.Find(key);
            if (dbRestaurant == null)
            {
                return NotFound();
            }

            patch.Put(dbRestaurant);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbRestaurantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(dbRestaurant);
        }

        // POST: odata/DbRestaurants
        public IHttpActionResult Post(DbRestaurant dbRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DbRestaurants.Add(dbRestaurant);
            db.SaveChanges();

            return Created(dbRestaurant);
        }

        // PATCH: odata/DbRestaurants(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<DbRestaurant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DbRestaurant dbRestaurant = db.DbRestaurants.Find(key);
            if (dbRestaurant == null)
            {
                return NotFound();
            }

            patch.Patch(dbRestaurant);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbRestaurantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(dbRestaurant);
        }

        // DELETE: odata/DbRestaurants(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            DbRestaurant dbRestaurant = db.DbRestaurants.Find(key);
            if (dbRestaurant == null)
            {
                return NotFound();
            }

            db.DbRestaurants.Remove(dbRestaurant);
            db.SaveChanges();

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

        private bool DbRestaurantExists(int key)
        {
            return db.DbRestaurants.Count(e => e.Id == key) > 0;
        }
    }
}
