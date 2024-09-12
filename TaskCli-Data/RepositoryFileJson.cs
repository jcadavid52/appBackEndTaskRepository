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

                    var serializeJson = JsonConvert.SerializeObject(tasks.TasksModel, Formatting.Indented);

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

        public HandlerResponse UpdateTask(string id, string description)
        {
            try
            {
                var tasks = GetTasks();

                if (tasks.TasksModel != null)
                {
                    var findTask = tasks.TasksModel.FirstOrDefault(t => t.Id == id);

                    if (findTask != null)
                    {
                        findTask.Description = description;
                        findTask.UpdatedAt = DateTime.Now;
                    } 
                    else
                    {

                        return new HandlerResponse
                        {
                            TaskModel = null,
                            ResponseResult = ResponseResult.TaskNotFound,
                            DescriptionResult = "No task found with the indicated id"
                        };
                    }

                    var jsonSerialize = JsonConvert.SerializeObject(tasks.TasksModel, Formatting.Indented);

                    File.WriteAllText(_pathJson, jsonSerialize);


                    return new HandlerResponse
                    {
                        TaskModel = findTask,
                        ResponseResult = ResponseResult.Success,
                        DescriptionResult = "Task updated successfully!"
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

        public HandlerResponse DeleteTask(string id)
        {
            try
            {
                var tasks = GetTasks();

                if (tasks.TasksModel != null)
                {
                    var findTask = tasks.TasksModel.FirstOrDefault(o => o.Id == id);

                    if (findTask != null)
                        tasks.TasksModel.Remove(findTask);
                    else
                    {

                        return new HandlerResponse
                        {
                            TaskModel = null,
                            ResponseResult = ResponseResult.TaskNotFound,
                            DescriptionResult = "No task found with the indicated id"
                        };
                    }


                    var jsonSerialize = JsonConvert.SerializeObject(tasks.TasksModel, Formatting.Indented);

                    File.WriteAllText(_pathJson, jsonSerialize);


                    return new HandlerResponse
                    {
                        TaskModel = findTask,
                        ResponseResult = ResponseResult.Success,
                        DescriptionResult = "Task deleted successfully!"
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

        public HandlerResponse UpdatedStatusTask(string id, string status)
        {
            try
            {
                var tasks = GetTasks();

                if (tasks.TasksModel != null)
                {
                    var findTask = tasks.TasksModel.FirstOrDefault(t => t.Id == id);

                    if (findTask != null)
                        findTask.Status = status;
                    else
                    {
                        return new HandlerResponse
                        {
                            TaskModel = null,
                            ResponseResult = ResponseResult.TaskNotFound,
                            DescriptionResult = "No task found with the indicated id"
                        };
                    }


                    var jsonSerialize = JsonConvert.SerializeObject(tasks.TasksModel, Formatting.Indented);

                    File.WriteAllText(_pathJson, jsonSerialize);

                   

                    return new HandlerResponse
                    {
                        TaskModel = findTask,
                        ResponseResult = ResponseResult.Success,
                        DescriptionResult = "Status task updated successfully!"
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

        public HandlerResponse FilterTasks(string status)
        {
            try
            {
                var tasks = GetTasks();

                if(tasks.TasksModel != null)
                {

                    var filterTasks = tasks.TasksModel.Where(o => o.Status == status).ToList();

                    return new HandlerResponse
                    {
                        TasksModel = filterTasks,
                        ResponseResult = ResponseResult.Success,
                        DescriptionResult = string.Empty

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
    }
}
