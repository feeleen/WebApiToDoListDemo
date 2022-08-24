using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SimpleWebApiToDoListDemo.Extentions;
using SimpleWebApiToDoListDemo.Wrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleWebApiToDoListDemo.Middleware.Exceptions
{
    public class GlobalExceptionHandlerMiddleware<T>
    {
        private readonly RequestDelegate nextDelegate;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext context, ILogger<T> logger)
        {
            try
            {
                await nextDelegate(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var response = context.Response;
                response.StatusCode = StatusCodes.Status200OK;
                response.ContentType = "application/json; charset=utf-8";

                HttpStatusCode status = HttpStatusCode.InternalServerError;

                switch (ex)
                {
                    case ArgumentException or 
                        ArgumentNullException or 
                        ArgumentOutOfRangeException:

                        status = HttpStatusCode.BadRequest;
                        break;
                }

                var result = JsonSerializer.Serialize(ResultWrapper.Fail(ex.StackTrace, GetExceptionsList(ex), status));
                await response.WriteAsync(result);
            }
        }

        private List<string> GetExceptionsList(Exception ex)
        {
            return ex.GetInnerExceptions().Select(x=> x.Message).ToList();
        }
    }
}
