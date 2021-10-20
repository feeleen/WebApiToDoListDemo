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
        public ToDoListController(ILogger<ToDoListController> logger) : base(logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DataFilter filter)
        {
            return await Execute(async () => 
            { 
                 return ResultWrapper.Success(await ToDoListService.GetRecordsAsync(filter));
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            return await Execute(async () =>
            {
                var todoItem = await ToDoListService.GetRecordAsync(id);

                return todoItem == null ? ResultWrapper.NotFound<ToDoList>() : ResultWrapper.Success(todoItem);
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(string todoName)
        {
            return await Execute(async () =>
            {
                var newTodoItem = await ToDoListService.InsertAsync(todoName);

                return newTodoItem == null ? ResultWrapper.Fail<ToDoList>() : ResultWrapper.Success(newTodoItem);
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put(ToDoList todoItem)
        {
            return await Execute(async () =>
            {
                var res = await ToDoListService.UpdateAsync(todoItem);

                return res > 0 ? ResultWrapper.Success(todoItem) : ResultWrapper.Fail<ToDoList>();
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return await Execute(async () =>
            {
                var res = await ToDoListService.DeleteAsync(id);

                return res == 0 ? ResultWrapper.NotFound() : ResultWrapper.Success();
            });
        }
    }
}
