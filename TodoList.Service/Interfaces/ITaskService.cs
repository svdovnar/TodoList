using TodoList.Domain.Entity;
using TodoList.Domain.Response;
using TodoList.Domain.ViewModels.Task;

namespace TodoList.Service.Interfaces
{
    public interface ITaskService
    {
        Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);
    }
}
