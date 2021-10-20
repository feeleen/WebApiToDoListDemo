using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleWebApiToDoListDemo;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApiToDoListDemo.Model;
using SimpleWebApiToDoListDemo.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleWebApiToDoListDemo.Wrapper;
using System.Net.Http;
using System.Text;
using System.Net.Http.Json;
using SimpleWebApiToDoListDemo.Data;
using LinqToDB;
using LinqToDB.Data;
using System.Linq;

namespace ToDoListTests
{
    [TestClass]
    public class DataTests : TestBase
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            testContext = context;
            factory = new WebApplicationFactory<Startup>();

            using (var db = new DbContext())
            {
                try
                {
                    db.Execute("DROP DATABASE IF EXISTS [ToDoListTest]");
                }
                catch { }

                try
                {
                    db.Execute("CREATE DATABASE [ToDoListTest]");
                }
                catch { }
                
                try
                {
                    db.DropTable<ToDoList>();
                }
                catch { }

                db.CreateTable<ToDoList>();
                db.InsertWithIdentity(new ToDoList() { Name = "First", IsComplete = false });
            }
        }
    }
}
