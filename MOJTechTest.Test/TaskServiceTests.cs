using Microsoft.EntityFrameworkCore;
using MOJTechTest.Data;
using MOJTechTest.Services;
using Shouldly;

namespace MOJTechTest.Test
{
    public class TaskServiceTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new AppDbContext(options);

            var task1 = new Models.TaskModel
            {
                case_id = "A6429",
                title = "Read Chapter 1, Section 1.1.3",
                status = "To do",
                due = DateTime.SpecifyKind(new DateTime(2025, 06, 14, 12, 00, 00), DateTimeKind.Utc)
            };
            context.tasks.Add(task1);
            context.SaveChanges();

            var task2 = new Models.TaskModel
            {
                case_id = "A6833",
                title = "Complete Quiz - ISO/OSI and TCP/IP models",
                status = "Done",
                due = DateTime.SpecifyKind(new DateTime(2025, 06, 18, 09, 30, 00), DateTimeKind.Utc)
            };
            context.tasks.Add(task2);
            context.SaveChanges();

            return context;
        }

        private AppDbContext GetUnseededDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            var context = new AppDbContext(options);

            return context;
        }

            [Fact]
        public void GetTaskFromCaseId()
        {
            // Arrange
            using var context = GetDbContext();
            var caseId = "A6429";
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTask(caseId);

            // Assert
            returnedTask.title.ShouldBe("Read Chapter 1, Section 1.1.3");
        }

        [Fact]
        public void GetTaskInvalidCaseId()
        {
            // Arrange
            using var context = GetDbContext();
            var caseId = "A0000";
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTask(caseId);

            // Assert
            returnedTask.ShouldBeNull();
        }

        [Fact]
        public void GetTaskFromId()
        {
            // Arrange
            using var context = GetDbContext();
            var id = 1;
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTask(id);

            // Assert
            returnedTask.title.ShouldBe("Read Chapter 1, Section 1.1.3");
        }

        [Fact]
        public void GetTaskInvalidId()
        {
            // Arrange
            using var context = GetDbContext();
            var id = -1;
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTask(id);

            // Assert
            returnedTask.ShouldBeNull();
        }

        [Fact]
        public void GetAllTasks()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTasks();

            // Assert
            returnedTask.Count.ShouldBe(2);
            returnedTask[0].case_id.ShouldBe("A6429");
            returnedTask[1].case_id.ShouldBe("A6833");
        }

        [Fact]
        public void GetAllTasksEmptyList()
        {
            // Arrange
            using var context = GetUnseededDbContext();
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.GetTasks();

            // Assert
            returnedTask.Count.ShouldBe(0);
        }

        [Fact]
        public void CreateTask()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            var newTask = new ViewModels.TaskModel
            {
                case_id = "A1745",
                title = "Complete the design a protocol activity",
                status = "In Progress",
                due = DateTime.SpecifyKind(new DateTime(2025, 06, 14, 12, 00, 00), DateTimeKind.Utc)
            };

            // Act
            var returnedTask = taskService.CreateTask(newTask);

            // Assert
            returnedTask.id.ShouldBe(3);
        }

        [Fact]
        public void UpdateTask()
        {
            // Arrange
            var id = 1;
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            var newTask = new ViewModels.TaskModel
            {
                case_id = "A7537",
                title = "Read - Understanding the OSI Seven Layer Model",
                status = "To do",
                due = DateTime.SpecifyKind(new DateTime(2025, 06, 15, 10, 00, 00), DateTimeKind.Utc)
            };

            // Act
            var returnedTask = taskService.UpdateTask(id, newTask);

            // Assert
            returnedTask.ShouldBe(1);
        }

        [Fact]
        public void UpdateTaskCannotFindTask()
        {
            // Arrange
            var id = -1;
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            var newTask = new ViewModels.TaskModel
            {
                case_id = "A7537",
                title = "Read - Understanding the OSI Seven Layer Model",
                status = "To do",
                due = DateTime.SpecifyKind(new DateTime(2025, 06, 15, 10, 00, 00), DateTimeKind.Utc)
            };

            // Act
            var returnedTask = taskService.UpdateTask(id, newTask);

            // Assert
            returnedTask.ShouldBeNull();
        }

        [Fact]
        public void DeleteTask()
        {
            // Arrange
            var id = 1;
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.DeleteTask(id);

            // Assert
            returnedTask.ShouldBe(1);
        }

        [Fact]
        public void DeleteTaskConnotFindTask()
        {
            // Arrange
            var id = -1;
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act
            var returnedTask = taskService.DeleteTask(id);

            // Assert
            returnedTask.ShouldBeNull();
        }
    }
}