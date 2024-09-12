using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli_Models;

namespace TaskCli_Services
{
    public interface IRepositoryFile
    {
        HandlerResponse GetTasks();
        HandlerResponse AddTask(TaskModel taskModel);
        HandlerResponse UpdateTask(string id, string description);
        HandlerResponse DeleteTask(string id);
        HandlerResponse UpdatedStatusTask(string id, string status);
        HandlerResponse FilterTasks(string status);
    }
}
