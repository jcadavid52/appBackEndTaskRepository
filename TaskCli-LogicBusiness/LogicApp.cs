using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_LogicBusiness
{
    public class LogicApp : ILogicApp
    {
        public HandlerResponse GetTasks()
        {
            List<TaskModel> taskModels = new List<TaskModel>();

            taskModels.Add(
            new TaskModel() 
            {
              Id = Guid.NewGuid().ToString(),
              Status = "todo",
              Description = "Tarea 1",
              CreatedAt = DateTime.Now,
              UpdatedAt = DateTime.Now,

            }
            
            );

            taskModels.Add(
            new TaskModel()
            {
                Id = Guid.NewGuid().ToString(),
                Status = "todo",
                Description = "Tarea 2",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            }

            );

            taskModels.Add(
            new TaskModel()
            {
                Id = Guid.NewGuid().ToString(),
                Status = "done",
                Description = "Tarea 3",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,

            }

            );

            var response = new HandlerResponse();

            response.ResponseResult = ResponseResult.Success;
            response.TasksModel = taskModels;
            response.DescriptionResult = "";

            return response;


        }
        public HandlerResponse AddTask(TaskModel taskModel)
        {
            throw new NotImplementedException();
        }

        
    }
}
