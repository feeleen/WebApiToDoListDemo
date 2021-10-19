using LinqToDB.Data;
using LinqToDB.Mapping;
using Microsoft.Extensions.Configuration;
using System;

namespace SimpleWebApiToDoListDemo.Data
{
    public class DbContext : DataConnection
    {
        public static IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public DbContext() : base("SqlServer", configuration.GetConnectionString("DefaultConnection"))
        {
        }
    }
}