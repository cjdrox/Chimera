using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace Chimera.Deploy
{
    public class Deployer
    {
        public bool UsesConnString { get; set; }
        public string ConnectionString { get; set; }

        public string SqlServerName { get; set; }
        public string InstanceName { get; set; }
        public string SqlServerLogin { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public Server Server { get; set; }

        public Deployer()
        {
            
        }

        public Deployer(string sqlServerLogin, string password, string instanceName, string databaseName)
        {
            SqlServerLogin = sqlServerLogin;
            Password = password;
            DatabaseName = databaseName;
            InstanceName = instanceName;
            UsesConnString = false;
        }

        public Deployer(string connectionString)
        {
            ConnectionString = connectionString;
            UsesConnString = true;
            InitProps();
        }

        public void InitProps()
        {
            if (UsesConnString)
            {
                string[] nameValuePairs = ConnectionString.Split(';');
                int i = 0;

                foreach (var paramString in nameValuePairs)
                {
                    string[] pair = paramString.Split('=');

                    switch (i)
                    {
                        case 0:
                            SqlServerName = pair[1];
                            break;
                        case 1:
                            DatabaseName = pair[1];
                            break;
                        case 2:
                            SqlServerLogin = pair[1];
                            break;
                        case 3:
                            Password = pair[1];
                            break;
                    }

                    i++;
                }
            }
        }

        public void DropAndRecreateDatabase(Func<string, int> callbackFunc)
        {
            try
            {
                Database db = null;

                if (Server == null)
                {
                    db = OpenConnection(callbackFunc);
                }
                
                if(Server == null)
                    throw new SqlServerManagementException("Error connecting to server.");

                //If the database exists, drop database
                callbackFunc("---Phase 1: drop/create DB---");

                if (db != null)
                {
                    callbackFunc("Database exists: dropping database...");
                    db.Drop();
                    callbackFunc("database dropped");
                }

                // Now re-create it.
                callbackFunc("Creating database...");
                db = new Database(Server, DatabaseName);
                db.Create();
                callbackFunc("Created database.");

                CloseConnection(callbackFunc);
            }
            catch (Exception exception)
            {
                callbackFunc(exception.Message);
            }
        }

        public Database OpenConnection(Func<string, int> callbackFunc)
        {
            try
            {
                callbackFunc("Connecting to server...");
                var srvConn = new ServerConnection(SqlServerName) { ServerInstance = @".\" + InstanceName };

                // Connecting to an instance of SQL Server using SQL Server Authentication
                Server = new Server(srvConn);
                Server.ConnectionContext.LoginSecure = false; // set to true for Windows Authentication
                Server.ConnectionContext.Login = SqlServerLogin;
                Server.ConnectionContext.Password = Password;
                Server.ConnectionContext.AutoDisconnectMode = AutoDisconnectMode.NoAutoDisconnect;
                Server.ConnectionContext.Connect();

                //The actual connection is made when a property is retrieved. 
                callbackFunc("Server connected: version " + Server.Information.Version);

                return Server.Databases[DatabaseName];
            }
            catch (Exception exception)
            {
                callbackFunc(exception.Message);
            }
            return null;
        }

        public void CloseConnection(Func<string, int> callbackFunc)
        {
            try
            {
                //Done
                callbackFunc("Disconnecting from server.");
                Server.ConnectionContext.Disconnect();
            }
            catch (Exception exception)
            {
                callbackFunc(exception.Message);
            }

        }

        public void CreateTables(Func<string, int> callbackFunc)
        {
            callbackFunc("---Phase 2: create tables---");

            Database db = OpenConnection(callbackFunc);

            var baseEntities = new List<Table>();
            var populator = new Populator();
            populator.Populate(baseEntities, db);

            foreach (var baseEntity in baseEntities)
            {
                CreateTable(baseEntity, callbackFunc);
            }

            CloseConnection(callbackFunc);
        }

        public void CreateTable(Table table, Func<string, int> callbackFunc)
        {
            callbackFunc("Creating table " + table.Name);
            table.Create();
        }
    }
}