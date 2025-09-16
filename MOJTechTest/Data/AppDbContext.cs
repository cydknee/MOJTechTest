using Microsoft.EntityFrameworkCore;
using MOJTechTest.Models;

namespace MOJTechTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskModel> tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .Property(t => t.id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskModel>().HasData(
                new TaskModel
                {
                    id = 1,
                    case_id = "A6429",
                    title = "Read Chapter 1, Section 1.1.3",
                    status = "To do",
                    due = DateTime.SpecifyKind(new DateTime(2025, 06, 12, 10, 30, 00), DateTimeKind.Utc)
                },
                new TaskModel
                {
                    id = 2,
                    case_id = "A1745",
                    title = "Complete the design a protocol activity",
                    status = "In Progress",
                    due = DateTime.SpecifyKind(new DateTime(2025, 06, 14, 12, 00, 00), DateTimeKind.Utc)
                },
                new TaskModel
                {
                    id = 3,
                    case_id = "A6833",
                    title = "Complete Quiz - ISO/OSI and TCP/IP models",
                    status = "Done",
                    due = DateTime.SpecifyKind(new DateTime(2025, 06, 19, 09, 00, 00), DateTimeKind.Utc)
                }
            );
        }

        public TaskModel GetTask(string caseId)
        {
            var taskInDatabase = tasks.Where(t => t.case_id.Equals(caseId))
                .FirstOrDefault();
            return taskInDatabase;
        }

        public TaskModel GetTask(int id)
        {
            var taskInDatabase = tasks.Find(id);
            return taskInDatabase;
        }

        public List<TaskModel> GetTasks()
        {
            var tasksInDatabase = tasks.OrderBy(t => t.id).ToList();
            return tasksInDatabase;
        }
    }
}
