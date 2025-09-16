using Microsoft.EntityFrameworkCore;
using MOJTechTest.Data;
using MOJTechTest.Models;

namespace MOJTechTest.Test
{
    public class TestBase: IDisposable
    {
        public AppDbContext context { get; set; }

        public AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            context = new AppDbContext(options);

            context.Database.EnsureCreated();

            context.tasks.AddRange(
                new TaskModel
                {
                    id = 1,
                    case_id = "A6429",
                    title = "Read Chapter 1, Section 1.1.3",
                    status = "To do",
                    due = DateTime.SpecifyKind(new DateTime(2025, 06, 14, 12, 00, 00), DateTimeKind.Utc)
                },
                new TaskModel
                {
                    id = 2,
                    case_id = "A6833",
                    title = "Complete Quiz - ISO/OSI and TCP/IP models",
                    status = "Done",
                    due = DateTime.SpecifyKind(new DateTime(2025, 06, 18, 09, 30, 00), DateTimeKind.Utc)
                }
            );
            context.SaveChanges();

            return context;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
