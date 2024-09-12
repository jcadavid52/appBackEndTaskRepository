

using TaskCli_Models;
using TaskCli_Services;

namespace TaskCli_Utils
{
    public class ManagementFileJson : IManagementFiles
    {
        private readonly string _pathJson = @"C:\Users\Camilo Cadavid\Documents\Projects\Personal projects\Task-Cli N Capas\TaskCli-App\tasks.json";

        public ManagementFileJson()
        {
            Environment.SetEnvironmentVariable("PathJson", _pathJson);

        }

        public HandlerResponse initializeFile()
        {
            var response = new HandlerResponse();

            try
            {
                if (!File.Exists(_pathJson))
                {
                    
                    using (StreamWriter writer = File.CreateText(_pathJson))
                    {
                        writer.Write("[]");
                        Console.WriteLine("Archivo creado: " + _pathJson);
                        response.DescriptionResult = "Archivo creado: " + _pathJson;
                        response.ResponseResult = ResponseResult.Success;
                    }

                    return response;
                }

                response.ResponseResult = ResponseResult.Success;
                response.DescriptionResult = "";

                return response;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error al inicializar el archivo JSON: " + ex.Message);

                response.ResponseResult = ResponseResult.FileWriteError;
                response.DescriptionResult = "Error al inicializar el archivo JSON:" + ex.Message;

                return response;

            }
        }
    }
}
