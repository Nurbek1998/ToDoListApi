namespace ToDoListApi.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class CreateUser
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }

    public class LoginUser
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
    }
}
