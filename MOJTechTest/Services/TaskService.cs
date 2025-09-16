using MOJTechTest.Data;
using System.ComponentModel.DataAnnotations;

namespace MOJTechTest.Services
{
    public interface ITaskService
    {
        ViewModels.TaskModel GetTask(string caseId);
        ViewModels.TaskModel GetTask(int caseId);
        List<ViewModels.TaskModel> GetTasks();
        ViewModels.TaskModel CreateTask(ViewModels.TaskModel taskModel);
        int? UpdateTask(int caseId, ViewModels.TaskModel taskModel);
        int? DeleteTask(int caseId);
    }

    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext taskContext)
        {
            _context = taskContext;
        }

        public ViewModels.TaskModel GetTask(string caseId)
        {
            var taskInDatabase = _context.GetTask(caseId);
            if ( taskInDatabase == null)
            {
                return null;
            }
            return new ViewModels.TaskModel(taskInDatabase);
        }

        public ViewModels.TaskModel GetTask(int id)
        {
            var taskInDatabase = _context.GetTask(id);
            if (taskInDatabase == null)
            {
                return null;
            }
            return new ViewModels.TaskModel(taskInDatabase);
        }

        public List<ViewModels.TaskModel> GetTasks()
        {
            var tasksInDatabase = _context.GetTasks();
            var taskViewModels = tasksInDatabase.Select(x => new ViewModels.TaskModel(x)).ToList();
            return taskViewModels;
        }

        public ViewModels.TaskModel CreateTask(ViewModels.TaskModel taskModel)
        {
            var taskToSave = new Models.TaskModel(taskModel.case_id, taskModel.title, taskModel.description, taskModel.status, taskModel.due);
            _context.tasks.Add(taskToSave);
            _context.SaveChanges();

            return new ViewModels.TaskModel(taskToSave);
        }

        public int? UpdateTask(int id, ViewModels.TaskModel taskModel)
        {
            var taskInDatabase = _context.GetTask(id);
            if (taskInDatabase == null)
            {
                return null;
            }
            var taskToSave = new Models.TaskModel(taskModel.case_id, taskModel.title, taskModel.description, taskModel.status, taskModel.due);
            taskInDatabase.UpdateFromViewModel(taskModel);

            return _context.SaveChanges();
        }

        public int? DeleteTask(int id)
        {
            var taskInDatabase = _context.GetTask(id);
            if (taskInDatabase == null)
            {
                return null;
            }
            _context.tasks.Remove(taskInDatabase);
            return _context.SaveChanges();
        }
    }
}
