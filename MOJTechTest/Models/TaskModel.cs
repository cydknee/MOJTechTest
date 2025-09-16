namespace MOJTechTest.Models
{
    public class TaskModel
    {
        public TaskModel() { }                
       
        public TaskModel(string caseId, string title, string? description, string status, DateTime due)
        {
            this.case_id = caseId;
            this.title = title;
            this.description = description;
            this.status = status;
            this.due = due;
        }

        public void UpdateFromViewModel(ViewModels.TaskModel taskModel)
        {
            this.case_id = taskModel.case_id;
            this.title = taskModel.title;
            this.description = taskModel.description;
            this.status = taskModel.status;
            this.due = taskModel.due;
        }

        public int id { get; set; }

        public string case_id { get; set; }

        public string title { get; set; } = string.Empty;

        public string? description { get; set; }

        public string status { get; set; } = "Pending";

        public DateTime due {get; set; }
    }
}
