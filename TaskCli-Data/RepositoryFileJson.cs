using Newtonsoft.Json;
using System.Threading.Tasks;
using TaskCli_Models;
using TaskCli_Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskCli_Data
{
    public class RepositoryFileJson : IRepositoryFile
    {
        private readonly string _pathJson = "";
        public RepositoryFileJson()
        {
            _pathJson = Environment.GetEnvironmentVariable("PathJson");

        }
        public HandlerResponse AddTask(TaskModel taskModel)
        {
            try
            {
                var tasks = GetTasks();

                if(tasks.TasksModel != null)
                {

                    tasks.TasksModel.Add(taskModel);

                    var serializeJson = JsonConvert.SerializeObject(tasks.values, Formatting.Indented);

                    File.WriteAllText(_pathJson, serializeJson);

                    return new HandlerResponse
                    {
                        TaskModel = taskModel,
                        ResponseResult = ResponseResult.Success,
                        DescriptionResult = "Task registered successfully!"
                    };
                }
                else
                {
                    return new HandlerResponse
                    {
                        TaskModel = null,
                        ResponseResult = ResponseResult.TaskNotFound,
                        DescriptionResult = "Error getting tasks"
                    };
                }

                

            }
            catch (Exception ex)
            {
                return new HandlerResponse
                {
                    TaskModel = null,
                    ResponseResult = ResponseResult.UnknownError,
                    DescriptionResult = "Error: " + ex.Message
                };
            }
        }

        public HandlerResponse GetTasks()
        {
            try
            {
                string jsonString = File.ReadAllText(_pathJson);

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonString);

                return new HandlerResponse
                {
                    TasksModel = tasks,
                    ResponseResult = ResponseResult.Success,
                    DescriptionResult = string.Empty

                };
            }
            catch (Exception ex)
            {


                return new HandlerResponse
                {
                    TasksModel = null,
                    ResponseResult = ResponseResult.UnknownError,
                    DescriptionResult = "Error interno: " + ex.Message.ToString()

                };
            }
        }

       
    }
}
