using TodoList.Domain.Entity.Enum;

namespace TodoList.Domain.ViewModels.Task
{
    public class CreateTaskViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException(Name, "Укадите название задачи");
            }
            if (string.IsNullOrWhiteSpace(Description))
            {
                throw new ArgumentNullException(Name, "Укадите описание задачи");
            }
        }

    }
}
