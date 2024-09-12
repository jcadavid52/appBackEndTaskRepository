using TaskCli_LogicBusiness;
using TaskCli_Models;
using TaskCli_Services;
using TaskCli_Utils;

ILogicApp logicApp = new LogicApp();
IManagementFiles file = new ManagementFileJson();

var response = file.initializeFile();
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
        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
else
{
    Console.WriteLine(response.DescriptionResult);  
}


