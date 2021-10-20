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
    public class MockToDoApiControllerTests
    {
        private static TestContext testContext;
        private static WebApplicationFactory<Startup> factory;

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

        [TestMethod]
        public async Task AnyRecordsExist()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("api/ToDoList?State=All");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<List<ToDoList>>>(result);
            Assert.IsTrue(obj.Data!= null);
            Assert.IsTrue(obj.Data.Count > 0);
            Assert.IsTrue(obj.Data[0].Name == "Test");
        }

        [TestMethod]
        public async Task ShouldReturnInsertedRecord()
        {
            var client = factory.CreateClient();

            var httpContent = new StringContent("=NewRecord", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync("api/ToDoList", httpContent);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<ToDoList>>(result);

            Assert.AreEqual("NewRecord", obj.Data.Name);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            factory.Dispose();
        }
    }
}
