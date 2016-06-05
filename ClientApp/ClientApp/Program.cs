using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    class Program
    {
        public static readonly string RestaurantUri = "http://localhost:50363/";
        public static readonly string LoginUri = "http://localhost:55805/";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Click 'r' to register new client, 'l' to login or 'q' to quit. [enter]");
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
                        while (true)
                        {
                            Console.WriteLine("Click 'l' to list all restaurants, 'r' to rate restaurant, 'a' to show rates for restaurants. 'e' to logout ");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.L)
                            {
                                listRestaurants();

                            }
                            else if (key.Key == ConsoleKey.R)
                            {
                                Console.ReadLine();
                                //todo
                                rateRestaurant(token);
                            }
                            else if (key.Key == ConsoleKey.A)
                            {
                                showRates();
                            }
                            else if (key.Key == ConsoleKey.E)
                            {
                                break;
                            }
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    Console.WriteLine("End of game");
                    Console.ReadKey();
                    break;
                }
            }

        }


        public static async void showRates()
        {
            var container = new ODataRestaurantClient.Default.Container(new Uri(RestaurantUri));
            Console.WriteLine("Rates:");
            Console.WriteLine("Rate : Restaurant : User");
            foreach (var rate in container.Rates)
            {
                using (var client = new HttpClient())
                {

                    var response = await client.GetAsync(LoginUri + "/users/name?id=" + rate.UserId.ToString());

                    var userName = await response.Content.ReadAsStringAsync();
                
                    Console.WriteLine("{0} : {1} : {2}", rate.Score, container.Restaurants.Where(x =>  x.Id == rate.RestaurantId).ToList().First().Name, userName);
                }
            }
            Console.WriteLine();
        }
        

        public static void rateRestaurant(int token)
        {

            var container = new ODataRestaurantClient.Default.Container(new Uri(RestaurantUri));
            //send to restaurant token, restaurantId and rate
            Console.WriteLine("Get id of restaurant you want to rate: [enter]");
            var restaurantId = Console.ReadLine();
            if (container.Restaurants.Where(x => x.Id == Int32.Parse(restaurantId)).ToList().Count() == 1)
            {
                var restaurant = container.Restaurants.Where(x => x.Id == Int32.Parse(restaurantId)).ToList().First();
                Console.WriteLine("Restaurant you have choosen:");
                Console.WriteLine("{0} : {1} : {2} : {3}", restaurant.Id, restaurant.Name, restaurant.Address, restaurant.Description);
                Console.WriteLine("Get new rate:");
                var rate = Console.ReadLine();
                //var resp = container.TestFunction().GetValue();
                //Console.WriteLine("Response to test function {0}", resp);
                // todo
                var containerRates = new ODataRestaurantClient.Default.Container(new Uri(RestaurantUri));
                //string param = "token=" + token + "&" + "restaurantId=" + restaurant.Id + "&" + "rate=" + rate;
                Console.WriteLine(containerRates.RateRestaurant(token.ToString(), restaurant.Id, Int32.Parse(rate)).GetValue());
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
            
        }

        public static void listRestaurants()
        {
            var container = new ODataRestaurantClient.Default.Container(new Uri(RestaurantUri));
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

            var request = (HttpWebRequest)WebRequest.Create(LoginUri + "/users/register");

            var postData = "Name=" + name;
            postData += "&Login=" + login;
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

        public static async void register2()
        {
            Console.WriteLine("Name");
            string name = Console.ReadLine();
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            using (var client = new HttpClient())
            {
                var registerObject = new Dictionary<string, string>
                {
                    { "Name", name },
                    { "Login", login },
                    { "Password", password }
                };

                var content = new FormUrlEncodedContent(registerObject);

                var response = await client.PostAsync(LoginUri + "/users/register", content);

                var responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("response status: {0}, content: {1}", response.StatusCode, responseString);
            }
        }

        public static int login()
        {
            int token = 0;
            Console.WriteLine("Login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

            var request = (HttpWebRequest)WebRequest.Create(LoginUri + "/users/login");

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
