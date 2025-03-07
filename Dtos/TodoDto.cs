namespace ToDoListApi.Dtos
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }
    }

    public class CreateTodoDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateTodoDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
