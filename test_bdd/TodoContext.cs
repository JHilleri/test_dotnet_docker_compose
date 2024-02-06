using Microsoft.EntityFrameworkCore;

namespace test_bdd
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
