using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_Data
{
    public class RepositoryFileJson : IRepositoryFile
    {
        private readonly string _pathJson;
        public RepositoryFileJson()
        {
            _pathJson = Environment.GetEnvironmentVariable("PathJson");

        }
        public HandlerResponse AddTask(TaskModel taskModel)
        {
            throw new NotImplementedException();
        }

        public HandlerResponse GetTasks()
        {
            try
            {
                string jsonString = File.ReadAllText(_pathJson);

                var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(jsonString);

                return (tasks, ResponseResult.Success, "");
            }
            catch (Exception ex)
            {

                return (null, ResponseResult.UnknownError, "Error interno: " + ex.Message.ToString());
            }
        }

       
    }
}
