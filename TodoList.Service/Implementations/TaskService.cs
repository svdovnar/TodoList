using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoList.DAL.Interfaces;
using TodoList.Domain.Entity;
using TodoList.Domain.Entity.Enum;
using TodoList.Domain.Response;
using TodoList.Domain.ViewModels.Task;
using TodoList.Service.Interfaces;

namespace TodoList.Service.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IBaseRepository<TaskEntity> _taskRepository;
        private ILogger<TaskService> _logger;
        public TaskService(IBaseRepository<TaskEntity> taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }
        public async Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model)
        {
            try
            {
                model.Validate();

                _logger.LogInformation($"Запрос на создание задачи- {model.Name}");
                var task = await _taskRepository.GetAll()
                    .Where(x=> x.Created.Date == DateTime.Today)
                    .FirstOrDefaultAsync(x => x.Name == model.Name);
                if (task == null)
                {
                    return new BaseResponse<TaskEntity>()
                    {
                        Description = "Задача с таким именем уже создана",
                        StatusCode = StatusCode.TaskIsHasAlready
                    };
                }
                task = new TaskEntity
                {
                    Name = model.Name,
                    Description = model.Description,
                    Priority = model.Priority,
                    Created = DateTime.Today,
                    IsCompleted = false
                };
                await _taskRepository.Create(task);

                _logger.LogInformation($"Задача создалась: {task.Name} {task.Created}");
                return new BaseResponse<TaskEntity>()
                {
                    Description = "Задача создалась",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TaskService.Created]: {ex.Message}");
                return new BaseResponse<TaskEntity>()
                {
                    Description = $"{ex.Message}",
                    StatusCode = StatusCode.EternalServerError
                };
            }

        }
    }
}
