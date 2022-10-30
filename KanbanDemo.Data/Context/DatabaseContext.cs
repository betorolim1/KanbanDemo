using KanbanDemo.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanDemo.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }
    }
}
