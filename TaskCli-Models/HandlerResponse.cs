namespace TaskCli_Models
{
    public enum ResponseResult
    {
        Success,
        TaskNotFound,
        FileWriteError,
        UnknownError
    }
    public class HandlerResponse
    {
        public List<TaskModel>? TasksModel;
        public TaskModel? TaskModel { get; set; }
        public ResponseResult ResponseResult { get; set; }
        public string DescriptionResult { get; set; }

    }
}
