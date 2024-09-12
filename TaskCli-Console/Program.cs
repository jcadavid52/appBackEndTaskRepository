using TaskCli_Data;
using TaskCli_LogicBusiness;
using TaskCli_Models;
using TaskCli_Services;
using TaskCli_Utils;

//args = new string[] { "update", "a094ea7b-d0db-4203-b501-3f1bbc8b3318","Tarea 2 modificada" };

IManagementFiles file = new ManagementFileJson();

var response = file.initializeFile();

IRepositoryFile repository = new RepositoryFileJson();
ILogicApp logicApp = new LogicApp(repository);





string option = args[0];

if (response.ResponseResult == ResponseResult.Success)
{
    

    switch (option)
    {
        case "list":
            var tasks = logicApp.GetTasks();

            if (tasks.ResponseResult == ResponseResult.Success && tasks.TasksModel != null)
            {
                if (tasks.TasksModel.Count > 0)
                {
                    foreach (var item in tasks.TasksModel)
                    {
                        Console.WriteLine($"ID: {item.Id} \n\n" +
                                          $"Description: {item.Description} \n\n" +
                                          $"Status: {item.Status} \n\n" +
                                          $"CreateAt: {item.CreatedAt} \n\n" +
                                          $"UpdatedAt: {item.UpdatedAt} \n\n---------------------------------------------------------------------- \n\n");
                    }


                }
                else
                    Console.WriteLine("No tasks");
            }
            else
            {
                Console.WriteLine(tasks.DescriptionResult);
            }
            break;
        case "add":
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 2)
                Console.WriteLine("Missing arguments for this function");
            else
            {
               
                var taskModel = new TaskModel();

                taskModel.Id = Guid.NewGuid().ToString();
                taskModel.Description = args[1];
                taskModel.Status = "todo";
                taskModel.CreatedAt = DateTime.Now;
                taskModel.UpdatedAt = DateTime.Now;

                var taskAdd = logicApp.AddTask(taskModel);

                if (taskAdd.ResponseResult == ResponseResult.Success && taskAdd.TaskModel != null)
                    Console.WriteLine(taskAdd.DescriptionResult + "\n\n" + $"ID: {taskAdd.TaskModel.Id} \n\n" +
                                              $"Description: {taskAdd.TaskModel.Description} \n\n" +
                                              $"Status: {taskAdd.TaskModel.Status} \n\n" +
                                              $"CreateAt: {taskAdd.TaskModel.CreatedAt} \n\n" +
                                              $"UpdatedAt: {taskAdd.TaskModel.UpdatedAt} \n\n");

                else
                    Console.WriteLine(taskAdd.DescriptionResult);
            }


            break;
        case "update":
            if (args.Length > 3)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 3)
                Console.WriteLine("Missing arguments for this function");
            else
            {
                var updatedTask = logicApp.UpdateTask(args[1], args[2]);

                if (updatedTask.ResponseResult == ResponseResult.Success && updatedTask.TaskModel != null)
                {



                    Console.WriteLine(updatedTask.DescriptionResult + "\n\n" + $"ID: {updatedTask.TaskModel.Id} \n\n" +
                                             $"Description: {updatedTask.TaskModel.Description} \n\n" +
                                             $"Status: {updatedTask.TaskModel.Status} \n\n" +
                                             $"CreateAt: {updatedTask.TaskModel.CreatedAt} \n\n" +
                                             $"UpdatedAt: {updatedTask.TaskModel.UpdatedAt} \n\n");
                }
                else if (updatedTask.ResponseResult == ResponseResult.TaskNotFound)
                    Console.WriteLine(updatedTask.DescriptionResult);
                else
                    Console.WriteLine(updatedTask.DescriptionResult);
            }
            break;
        case "delete":
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 2)
                Console.WriteLine("Missing arguments for this function");
            else
            {
                var deletedTask = logicApp.DeleteTask(args[1]);

                if (deletedTask.ResponseResult == ResponseResult.Success)
                    Console.WriteLine(deletedTask.DescriptionResult);
                else if (deletedTask.ResponseResult == ResponseResult.TaskNotFound)
                    Console.WriteLine(deletedTask.DescriptionResult);
                else
                    Console.WriteLine(deletedTask.DescriptionResult);


            }
            break;
        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
else
{
    Console.WriteLine(response.DescriptionResult);  
}


