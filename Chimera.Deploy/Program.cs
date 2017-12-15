using System;
using System.Configuration;

namespace Chimera.Deploy
{
    public class Program
    {
        static void Main()
        {
            var connString = ConfigurationManager.ConnectionStrings["cs"];
            var deployer = new Deployer(connString.ConnectionString);
            
            Output("Starting deployer...");
            
            deployer.DropAndRecreateDatabase(Output);
            deployer.CreateTables(Output);

            Output("Press any key to exit...");
            Console.ReadKey();
        }

        static int Output(string s)
        {
            Console.WriteLine(s);
            return 0;
        }
    }
}
