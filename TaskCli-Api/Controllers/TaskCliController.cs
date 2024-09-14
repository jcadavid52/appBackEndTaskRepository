using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskCli_Api.RequestModels;
using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCliController : ControllerBase
    {
        private readonly ILogicApp _logicApp;

        public TaskCliController(ILogicApp logicApp)
        {
            _logicApp = logicApp;
        }

        [HttpGet("GetTasks")]
        public IActionResult Get()
        {
            var tasksResponse = _logicApp.GetTasks();

            if(tasksResponse.ResponseResult == TaskCli_Models.ResponseResult.Success)
            {
                return Ok(tasksResponse.TasksModel);
            }
            else
            {
                return StatusCode(500, tasksResponse.DescriptionResult);
            }
           
        }

        [HttpGet("FilterTasks")]
        public IActionResult Get(string status = null)
        {
            var filterResponse = _logicApp.FilterTasks(status);

            if (filterResponse.ResponseResult == TaskCli_Models.ResponseResult.Success && filterResponse.TasksModel != null) 
            {
                if(filterResponse.TasksModel.Count > 0)
                {
                    return StatusCode(200, filterResponse.TasksModel);
                }
                else
                {
                    return StatusCode(200,"No tasks");
                }

                
            }else if(filterResponse.ResponseResult == TaskCli_Models.ResponseResult.TaskNotFound)
            {
                return StatusCode(500, filterResponse.DescriptionResult);
            }
            else
            {
                return StatusCode(500, filterResponse.DescriptionResult);
            }

            

        }

        [HttpPost("AddTask")]
        public IActionResult Post([FromBody] TaskAddModelRequest taskModelRequest)
        {
            if (string.IsNullOrEmpty(taskModelRequest.Description))
            {
                return BadRequest("La descripción no puede estar vacía.");
            }
            var taskModel = new TaskModel();

            taskModel.Id = Guid.NewGuid().ToString();
            taskModel.Description = taskModelRequest.Description;
            taskModel.Status = "todo";
            taskModel.CreatedAt = DateTime.Now;
            taskModel.UpdatedAt = DateTime.Now;

            var taskAdd = _logicApp.AddTask(taskModel);



            if (taskAdd.ResponseResult == ResponseResult.Success)
            { 

                return StatusCode(200, new
                {
                    Description = taskAdd.DescriptionResult,
                 
                    Task = taskAdd.TaskModel
                });
            }else
            {
                return StatusCode(500, taskAdd.DescriptionResult);
            }

         
        }


    }
}
