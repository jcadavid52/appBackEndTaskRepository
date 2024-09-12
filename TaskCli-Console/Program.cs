﻿using TaskCli_LogicBusiness;
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

            if (tasks.ResponseResult == ResponseResult.Success)
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
        default:
            Console.WriteLine("Invalid option");
            break;
    }
}
else
{
    Console.WriteLine(response.DescriptionResult);  
}


