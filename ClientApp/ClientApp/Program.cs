using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Click 'r' to register new client or 'l' to login. [enter]");
                var key = Console.ReadKey();
                Console.ReadLine();
                if (key.Key == ConsoleKey.R)
                {
                    register();
                }
                else if (key.Key == ConsoleKey.L)
                {
                    int token = login();
                    if (token > 0)
                    {
                        Console.WriteLine("Click 'l' to list all restaurants, 'r' to rate restaurant");
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.L)
                        {
                            listRestaurants();
                            
                        }
                        else if (key.Key == ConsoleKey.R)
                        {
                            Console.WriteLine("Check id of restaurant which you'd like to rate");
                            key = Console.ReadKey();
                            //todo
                            //rateRestaurant(token);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

        }


        public static void rateRestaurant(int token)
        {
            string restaurantUri = "http://localhost:50363/";
            var container = new ODataRestaurantClient.Default.Container(new Uri(restaurantUri));
            //send to restaurant token, restaurantId and rate
        }

        public static void listRestaurants()
        {
            string restaurantUri = "http://localhost:50363/";
            var container = new ODataRestaurantClient.Default.Container(new Uri(restaurantUri));
            Console.WriteLine("All restaurants");
            foreach (var restaurant in container.Restaurants)
            {
                Console.WriteLine("{0} : {1} : {2} : {3}", restaurant.Id, restaurant.Name, restaurant.Address, restaurant.Description);
            }
            Console.WriteLine();
        }


        public static void register()
        {
            Console.WriteLine("Name");
            string name = Console.ReadLine();
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:55805/users/register");

            var postData = "Name=" + name;
            postData += "Login=" + login;
            postData += "&Password=" + password;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == (HttpStatusCode) 200)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Console.WriteLine("Response = {0}", responseString);
                }
                else
                {
                    Console.WriteLine("Bad request");
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException());
                e.GetBaseException();
            }
            Console.WriteLine();
            Console.ReadLine();
        }

        public static int login()
        {
            int token = 0;
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:55805/users/login");

            var postData = "Login=" + login;
            postData += "&Password=" + password;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == (HttpStatusCode) 200)
                {
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    Console.WriteLine("Response = {0}", responseString);
                    token = getTokenFromResponse(responseString);
                }
                else
                {
                    Console.WriteLine("Bad request");
                    return 0;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException());
                e.GetBaseException();
                return 0;
            }
            Console.WriteLine();
            Console.ReadLine();
            return token;
        }

        public static int getTokenFromResponse(string response)
        {
            char separator = '"';
            var elements = response.Split(separator);
            string token = "";
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Equals("Token"))
                {
                    token = elements[i + 2].ToString();
                    break;
                }
            }
            int tokenInt = int.Parse(token);

            return tokenInt;
        }
    }
}
