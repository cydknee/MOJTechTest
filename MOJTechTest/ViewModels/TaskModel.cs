namespace MOJTechTest.ViewModels
{
    public class TaskModel
    {
        public TaskModel() { } //only here for model binding. don't use it in code.

        public TaskModel(Models.TaskModel taskModel)
        {
            id = taskModel.id;
            case_id = taskModel.case_id;
            title = taskModel.title;
            description = taskModel.description;
            status = taskModel.status;
            due = taskModel.due;
        }

        public int id { get; set; }

        public string case_id { get; set; }

        public string title { get; set; }

        public string? description { get; set; }

        public string status { get; set; }

        public DateTime due { get; set; }
    }
}
