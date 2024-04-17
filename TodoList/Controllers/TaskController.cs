using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.ViewModels.Task;
using TodoList.Service.Interfaces;


namespace TodoList.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
          
            var response = await _taskService.Create(model);
            if (response.StatusCode == Domain.Entity.Enum.StatusCode.OK)
            {
                return Ok(new {description = response.Description});
            }
            return BadRequest( new { description = response.Description });
        }
    }
}
