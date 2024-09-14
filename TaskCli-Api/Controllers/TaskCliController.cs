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
        public IActionResult Post([FromBody] TaskModelRequest taskModelRequest)
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
            }else if(taskAdd.ResponseResult == ResponseResult.TaskNotFound)
            {
                return StatusCode(500, taskAdd.DescriptionResult);
            }
            else
            {
                return StatusCode(500, taskAdd.DescriptionResult);
            }

         
        }

        [HttpPut("UpdateTask")]
        public IActionResult Update(string id, [FromBody] TaskModelRequest taskModelRequest)
        {
          

            if (string.IsNullOrEmpty(taskModelRequest.Description))
                return BadRequest("La descripción no puede estar vacía.");

            var taskUpdate = _logicApp.UpdateTask(id, taskModelRequest.Description);


            if (taskUpdate.ResponseResult == ResponseResult.Success)
            {

                return StatusCode(200, new
                {
                    Description = taskUpdate.DescriptionResult,

                    Task = taskUpdate.TaskModel
                });
            }
            else if (taskUpdate.ResponseResult == ResponseResult.TaskNotFound)
            {
                return StatusCode(500, taskUpdate.DescriptionResult);
            }
            else
            {
                return StatusCode(500, taskUpdate.DescriptionResult);
            }



        }

        [HttpPut("UpdateStatusTask")]
        public IActionResult UpdateStatusTask(string id, [FromBody] ModelRequestUpdateStatusTask modelRequestUpdateStatusTask)
        {
            if (string.IsNullOrEmpty(modelRequestUpdateStatusTask.Status))
                return BadRequest("El campo status no puede estar vacío");

            var response = _logicApp.UpdatedStatusTask(id, modelRequestUpdateStatusTask.Status);

            if (response.ResponseResult == ResponseResult.Success)
            {

                return StatusCode(200, new
                {
                    Description = response.DescriptionResult,

                    Task = response.TaskModel
                });
            }
            else if (response.ResponseResult == ResponseResult.TaskNotFound)
            {
                return StatusCode(500, response.DescriptionResult);
            }
            else
            {
                return StatusCode(500, response.DescriptionResult);
            }

        }

    }
}
