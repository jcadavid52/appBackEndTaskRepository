using TaskCli_Models;

namespace TaskCli_Services
{
    public interface ILogicApp
    {
       HandlerResponse GetTasks();
       HandlerResponse AddTask(TaskModel taskModel);
    }
}
