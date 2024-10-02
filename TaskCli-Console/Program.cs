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
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length == 2)
            {
                var tasksFilter = logicApp.FilterTasks(args[1]);

                if (tasksFilter != null && tasksFilter.ResponseResult == ResponseResult.Success)
                {
                    if (tasksFilter.TasksModel.Count > 0)
                    {
                        foreach (var item in tasksFilter.TasksModel)
                        {
                            Console.WriteLine($"ID: {item.Id} \n\n" +
                                              $"Description: {item.Description} \n\n" +
                                              $"Status: {item.Status} \n\n" +
                                              $"CreateAt: {item.CreatedAt} \n\n" +
                                              $"UpdatedAt: {item.UpdatedAt} \n\n---------------------------------------------------------------------- \n\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results found");
                    }
                }



            }
            else
            {
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
            }
            
            break;
        case "add":
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 2)
                Console.WriteLine("Missing arguments for this function");
            else
            {

                string hour = DateTime.Now.ToString("HH:mm");
                string date = DateTime.Today.ToShortDateString().ToString();
                string fullDate = date + " " + hour;

                var taskModel = new TaskModel();

                taskModel.Id = Guid.NewGuid().ToString();
                taskModel.Description = args[1];
                taskModel.Status = "todo";
                taskModel.CreatedAt = fullDate;
                taskModel.UpdatedAt = fullDate;

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
        case "mark-in-progress":
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 2)
                Console.WriteLine("Missing arguments for this function");
            else
            {
                var statusProgress = logicApp.UpdatedStatusTask(args[1], "in-progress");

                if (statusProgress.ResponseResult == ResponseResult.Success)
                    Console.WriteLine(statusProgress.DescriptionResult);
                else if (statusProgress.ResponseResult == ResponseResult.TaskNotFound)
                    Console.WriteLine(statusProgress.DescriptionResult);
                else
                    Console.WriteLine(statusProgress.DescriptionResult);
            }
            break;
        case "mark-done":
            if (args.Length > 2)
                Console.WriteLine("The number of arguments exceeds those requested for this function.");
            else if (args.Length < 2)
                Console.WriteLine("Missing arguments for this function");
            else
            {
                var statusDone = logicApp.UpdatedStatusTask(args[1], "done");

                if (statusDone.ResponseResult == ResponseResult.Success)
                    Console.WriteLine(statusDone.DescriptionResult);
                else if (statusDone.ResponseResult == ResponseResult.TaskNotFound)
                    Console.WriteLine(statusDone.DescriptionResult);
                else
                    Console.WriteLine(statusDone.DescriptionResult);
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


