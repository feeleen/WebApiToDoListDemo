using Microsoft.AspNetCore.Builder;
using SimpleWebApiToDoListDemo.Controllers;
using SimpleWebApiToDoListDemo.Middleware.Exceptions;

namespace SimpleWebApiToDoListDemo.Extentions
{
    public static class AppBuilderExtentions
    {

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware<ToDoListController>>();
        }
    }
}
