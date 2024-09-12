using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_Data
{
    public class RepositorySqlServer : IRepositorySQL
    {
        public void Connection()
        {
            Console.WriteLine("Conectando a SQL Server");
        }
        public HandlerResponse AddTask(TaskModel taskModel)
        {
            throw new NotImplementedException();
        }

        public HandlerResponse DeleteTask(string id)
        {
            throw new NotImplementedException();
        }

        public HandlerResponse FilterTasks(string status)
        {
            throw new NotImplementedException();
        }

        public HandlerResponse GetTasks()
        {
            throw new NotImplementedException();
        }

        public HandlerResponse UpdatedStatusTask(string id, string status)
        {
            throw new NotImplementedException();
        }

        public HandlerResponse UpdateTask(string id, string description)
        {
            throw new NotImplementedException();
        }
    }
}
