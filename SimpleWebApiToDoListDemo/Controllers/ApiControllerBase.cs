using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApiToDoListDemo.Model;
using SimpleWebApiToDoListDemo.Services;
using SimpleWebApiToDoListDemo.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWebApiToDoListDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]"), Produces("application/json")]
    public class ApiControllerBase<T> : ControllerBase
    {
        protected readonly ILogger<T> logger;

        public ApiControllerBase(ILogger<T> logger)
        {
            this.logger = logger;
        }
    }
}
