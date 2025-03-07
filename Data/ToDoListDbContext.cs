using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ToDoListApi.Entities;

namespace ToDoListApi.Data
{
    public class ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
