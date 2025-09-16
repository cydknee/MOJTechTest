using Microsoft.AspNetCore.Mvc;
using MOJTechTest.Services;

namespace MOJTechTest.Controllers
{
    [Route("mojtechtest/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: mojtechtest/task/caseid/A9999
        [HttpGet("caseid/{caseId}")]
        public IActionResult GetTaskByCaseId([FromRoute] string caseId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.GetTask(caseId);
            if (result == null)
            {
                return NotFound(new { message = $"Could not find case id {caseId}." });
            }
            return Ok(result);
        }

        // GET: mojtechtest/task/5
        [HttpGet("{id}")]
        public IActionResult GetTask([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.GetTask(id);
            return Ok(result);
        }

        // GET: mojtechtest/task
        [HttpGet]
        public IActionResult GetTasks()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.GetTasks();
            return Ok(result);
        }

        // POST: mojtechtest/task
        [HttpPost]
        public IActionResult Post([FromBody] ViewModels.TaskModel taskModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.CreateTask(taskModel);
            return Ok(result);
        }

        // PUT: mojtechtest/task/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ViewModels.TaskModel taskModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.UpdateTask(id, taskModel);
            return Ok(result);
        }

        // DELETE: mojtechtest/task/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _taskService.DeleteTask(id);
            if (result == null)
            {
                return NotFound(new { message = $"Could not find task {id} to delete." });
            }
            return Ok(result);
        }
    }
}
