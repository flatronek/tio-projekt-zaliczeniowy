using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RestaurantService.Models;

namespace RestaurantClient.Controllers
{
    public class RestaurantsController : ApiController
    {
            List<Restaurant> restaurants = new List<Restaurant> {
        new Restaurant{ Id =1, Name="Pod psem", Description="Wege", Address="Krakow"},
        new Restaurant{ Id =2, Name="Pod dabem", Description="Mieso", Address="Warszawa"}
    };

            // GET api/<controller>
            public IEnumerable<Restaurant> Get()
            {
                return restaurants;
            }

            // GET api/<controller>/5
            public Restaurant Get(int id)
            {
                return restaurants.SingleOrDefault(m => m.Id == id);
            }

            // POST api/<controller>
            public void Post([FromBody]Restaurant restaurant)
            {
                restaurants.Add(restaurant);
                // Save changes
            }

            // PUT api/<controller>/5
            public void Put(int id, [FromBody]Restaurant restaurant)
            {
                Restaurant existingRestaurant = restaurants.SingleOrDefault(m => m.Id == id);
                existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Address = restaurant.Address;
            existingRestaurant.Description = restaurant.Description;
                // Save changes
            }

            // DELETE api/<controller>/5
            public void Delete(int id)
            {
                restaurants.Remove(restaurants.SingleOrDefault(m => m.Id == id));
                // Save changes
            }
        }
}
