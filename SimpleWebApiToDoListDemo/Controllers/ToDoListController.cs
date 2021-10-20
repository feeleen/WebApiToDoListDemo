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
    public class ToDoListController : ApiControllerBase<ToDoListController>
    {
        private readonly IToDoListService todoService;
        public ToDoListController(ILogger<ToDoListController> logger, IToDoListService todoService) : base(logger)
        {
            this.todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DataFilter filter)
        {
            return await Execute(async () => 
            { 
                 return ResultWrapper.Success(await todoService.GetRecordsAsync(filter));
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return await Execute(async () =>
            {
                var todoItem = await todoService.GetRecordAsync(id);

                return todoItem == null ? ResultWrapper.NotFound<ToDoList>() : ResultWrapper.Success(todoItem);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm, FromBody] string todoName)
        {
            return await Execute(async () =>
            {
                var newTodoItem = await todoService.InsertAsync(todoName);

                return newTodoItem == null ? ResultWrapper.Fail<ToDoList>() : ResultWrapper.Success(newTodoItem);
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(ToDoList todoItem)
        {
            return await Execute(async () =>
            {
                var res = await todoService.UpdateAsync(todoItem);

                return res > 0 ? ResultWrapper.Success(todoItem) : ResultWrapper.Fail<ToDoList>();
            });
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            return await Execute(async () =>
            {
                var res = await todoService.DeleteAsync(id);

                return res == 0 ? ResultWrapper.NotFound() : ResultWrapper.Success();
            });
        }
    }
}
