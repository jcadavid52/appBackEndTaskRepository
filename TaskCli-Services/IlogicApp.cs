using TaskCli_Models;

namespace TaskCli_Services
{
    public interface ILogicApp
    {
       HandlerResponse GetTasks();
       HandlerResponse AddTask(TaskModel taskModel);
       HandlerResponse UpdateTask(string id, string description);
       HandlerResponse DeleteTask(string id);
    }
}
