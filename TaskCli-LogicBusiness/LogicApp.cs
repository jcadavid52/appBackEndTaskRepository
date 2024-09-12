using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_LogicBusiness
{
    public class LogicApp : ILogicApp
    {
        private readonly IRepositoryFile _repositoryFile;

        public LogicApp(IRepositoryFile repositoryFile)
        {
            _repositoryFile = repositoryFile;
        }
        public HandlerResponse GetTasks()
        {
            var response = _repositoryFile.GetTasks();

            return response;
        }
        public HandlerResponse AddTask(TaskModel taskModel)
        {
            var response = _repositoryFile.AddTask(taskModel);

            return response;
        }

        public HandlerResponse UpdateTask(string id, string description)
        {
            var response = _repositoryFile.UpdateTask(id,description);

            return response;
        }
    }
}
