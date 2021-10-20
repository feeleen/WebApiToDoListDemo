using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SimpleWebApiToDoListDemo;
using SimpleWebApiToDoListDemo.Model;
using SimpleWebApiToDoListDemo.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListTests
{
    public class TestBase
    {
        protected static TestContext testContext;
        protected static WebApplicationFactory<Startup> factory;

        [TestMethod]
        public virtual async Task Test01AnyFirstRecordsExist()
        {
            var client = factory.CreateClient();
            var response = await client.GetAsync("api/ToDoList?State=All");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<List<ToDoList>>>(result);
            Assert.IsTrue(obj.Data != null);
            Assert.IsTrue(obj.Data.Count > 0);
            Assert.IsTrue(obj.Data.Where(x => x.Name == "First").Any());
        }

        [TestMethod]
        public virtual async Task Test02ShouldReturnInsertedRecord()
        {
            var client = factory.CreateClient();

            var response = await client.PostAsync("api/ToDoList", new StringContent("=NewRecord", Encoding.UTF8, "application/x-www-form-urlencoded"));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<ToDoList>>(result);

            Assert.AreEqual("NewRecord", obj.Data.Name);
        }

        [TestMethod]
        public virtual async Task Test03DeleteRecord()
        {
            var client = factory.CreateClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "api/ToDoList/1");
            message.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(message);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<object>>(result);

            Assert.AreEqual(true, obj.IsSuccess);
        }

        [TestMethod]
        public virtual async Task Test03DeleteRecordNotExists()
        {
            var client = factory.CreateClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "api/ToDoList/23423234234");
            message.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(message);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<object>>(result);

            Assert.AreEqual((int)HttpStatusCode.NotFound, obj.ResultCode);
        }

        [TestMethod]
        public virtual async Task Test03DeleteRecordNegativeNumber()
        {
            var client = factory.CreateClient();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Delete, "api/ToDoList/-1");
            message.Content = new StringContent(string.Empty, Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.SendAsync(message);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ResultWrapper<object>>(result);

            Assert.AreEqual((int)HttpStatusCode.BadRequest, obj.ResultCode);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            factory.Dispose();
        }
    }
}
