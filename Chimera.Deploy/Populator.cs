using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Chimera.Base;
using Chimera.DataAcess;
using Microsoft.SqlServer.Management.Smo;

namespace Chimera.Deploy
{
    public class Populator
    {
        private readonly List<Type> _types = new List<Type>();
        private readonly List<Table> _tables = new List<Table>();
 
        public List<Type> GetTypes()
        {
            return _types;
        }

        public List<Table> GetTables()
        {
            return _tables;
        } 

        public Populator()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(BaseEntity));
            _types.AddRange(assembly.GetTypes().Where(c => c.IsSubclassOf(typeof(BaseEntity))));

            var path = Directory.GetCurrentDirectory();
            var entityFile = ConfigurationManager.AppSettings["EntityPath"];

            assembly = Assembly.LoadFrom(path + "\\" + entityFile);
            _types.AddRange(assembly.GetTypes().Where(c => c.IsSubclassOf(typeof(BaseEntity))));
        }

        public void Populate(List<Table> baseEntities, Database database)
        {
            _tables.Clear();

            foreach (var classInfo in _types)
            {
                var noSerialize = classInfo.GetCustomAttributes(typeof(NoSerializeAttribute), true).Any();

                if (noSerialize)
                {
                    continue;
                }

                var table = new Table(database, classInfo.Name);

                PropertyInfo[] props = classInfo.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    // Check for custom length
                    var serializeAtts = prop.GetCustomAttributes(typeof (SerializeAttribute), true);
                    int? length = null;

                    if (serializeAtts.Any())
                    {                       
                        length = (int) ((SerializeAttribute) serializeAtts.First()).Length;
                    }

                    DataType type = TypeHelper.GetSqlType(prop.PropertyType, length);
                    string name = prop.Name;

                    var column = new Column(table, name, type);
                    
                    table.Columns.Add(column);
                }

                baseEntities.Add(table);
                _tables.Add(table);
            }
        }
    }
}