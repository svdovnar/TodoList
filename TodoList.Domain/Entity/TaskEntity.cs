using TodoList.Domain.Entity.Enum;

namespace TodoList.Domain.Entity
{
    public class TaskEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public Priority Priority { get; set; }

    }
}
