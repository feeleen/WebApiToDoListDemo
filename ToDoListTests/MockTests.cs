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

namespace ToDoListTests
{
    [TestClass]
    public class MockToDoApiControllerTests : TestBase
    {
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            testContext = context;
            factory = new WebApplicationFactory<Startup>();

            factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseSetting("https_port", "5001").UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddScoped<IToDoListService, MockToDoListService>();
                });
            });
        }
    }
}
