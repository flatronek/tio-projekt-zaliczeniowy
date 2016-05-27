using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TokenClient.TokenServiceReference;

namespace TokenClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenServiceClient tokenClient = new TokenServiceClient();

            TokenObject token = tokenClient.createTokenForUser(3);
            if (token == null)
            {
                Console.WriteLine("This user doesn't exist in db.");
            }
            else
            {
                Console.WriteLine("Created token: {0} for user with id: {1}", token.Token, token.UserId);
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
