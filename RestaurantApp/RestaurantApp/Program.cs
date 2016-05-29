using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp
{
    class Program
    {
        public static readonly string RestaurantUri = "http://localhost:50363/";

        static void Main(string[] args)
        {
            var container = new RestaurantApp.Default.Container(new Uri(RestaurantUri));
            Console.WriteLine("Dodawanie restuaracji");
            container.AddToRestaurants(new RestaurantService.Models.Restaurant() { Id = 1, Name = "Pod grusza", Address = "Krakow", Description = "Wege" });
            var serviceResponse = container.SaveChanges();

            while (true)
            {
                Console.WriteLine("Click 'r' to display all restaurants.");
                var key = Console.ReadKey();
                Console.ReadLine();
                if (key.Key == ConsoleKey.R)
                {
                    DisplayRestaurants();
                }
                else
                    continue;
              
            }
        }

        public static void DisplayRestaurants()
        {
            var container = new RestaurantApp.Default.Container(new Uri(RestaurantUri));
            Console.WriteLine("All restaurants");
            foreach (var restaurant in container.Restaurants)
            {
                Console.WriteLine("{0} : {1} : {2} : {3}", restaurant.Id, restaurant.Name, restaurant.Address, restaurant.Description);
            }
            Console.WriteLine();
        }
    }
}
