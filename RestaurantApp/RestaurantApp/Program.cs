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
           
      
            while (true)
            {
                Console.WriteLine("1. Click 'a' to add new restaurant.");
                Console.WriteLine("2. Click 'r' to display all restaurants.");
                Console.WriteLine("3. Clik 'd' to delete a restaurant.");

                var key = Console.ReadKey();
                Console.ReadLine();

                if (key.Key == ConsoleKey.A)
                {
                    Console.WriteLine("Name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Address: ");
                    string address = Console.ReadLine();

                    Console.WriteLine("Description: ");
                    string description = Console.ReadLine();

                    AddingRestaurant(name, address, description);

                }
                else if (key.Key == ConsoleKey.R)
                {
                    DisplayRestaurants();
                }

                else if (key.Key == ConsoleKey.D)
                {
                    Console.WriteLine("Write Id of restaurant which you want to delete: ");
                    var l = Console.ReadKey();
                    Console.ReadLine();
                    if(char.IsDigit(l.KeyChar))
                    {
                        int result = l.KeyChar - '0';
                        DeleteRestaurants(result);
                    }
                   
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

        public static void AddingRestaurant(string name, string address, string description)
        {
            var container = new RestaurantApp.Default.Container(new Uri(RestaurantUri));
            container.AddToRestaurants(new RestaurantService.Models.Restaurant() { Name = name, Address = address, Description = description });
            var serviceResponse = container.SaveChanges();

            foreach (var operationResponse in serviceResponse)
            {

                Console.WriteLine("Response: {0}", operationResponse.StatusCode);
            }
           

        }
        
        public static void DeleteRestaurants(int id)
        {
           
                var container = new RestaurantApp.Default.Container(new Uri(RestaurantUri));

                container.Restaurants.Where(x => x.Id == id).ToList().ForEach(x => container.DeleteObject(x));
                var serviceResponse = container.SaveChanges();
                foreach (var operationResponse in serviceResponse)
                {

                    Console.WriteLine("Response: {0}", operationResponse.StatusCode);
                }
            
        }

        
    }
}
