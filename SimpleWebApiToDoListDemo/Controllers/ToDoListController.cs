using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleWebApiToDoListDemo.Model;
using SimpleWebApiToDoListDemo.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWebApiToDoListDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> logger;

        public ToDoListController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoList>>> Get([FromQuery] DataFilter filter)
        {
            try
            {
                return await ToDoListService.GetRecordsAsync(filter);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, filter);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoList>> Get(long id)
        {
            try
            {
                var todoItem = await ToDoListService.GetRecordAsync(id);

                return (todoItem == null) ? NotFound() : new ObjectResult(todoItem);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ToDoList>> Post(string todoName)
        {
            try
            {
                var newTodoItem = await ToDoListService.InsertAsync(todoName);

                return newTodoItem == null ? BadRequest() : Ok(newTodoItem);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, todoName);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ToDoList>> Put(ToDoList todoItem)
        {
            try
            {
                var res = await ToDoListService.UpdateAsync(todoItem);

                return res > 0 ? Ok(todoItem) : NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, todoItem);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                var res = await ToDoListService.DeleteAsync(id);

                return res == 0 ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, id);
                return BadRequest(ex.Message);
            }
        }
    }
}
