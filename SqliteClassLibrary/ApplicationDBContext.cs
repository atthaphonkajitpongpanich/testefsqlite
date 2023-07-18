
using Microsoft.EntityFrameworkCore;
using SqliteClassLibrary.Models;

namespace SqliteClassLibrary
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite();

        public DbSet<Item> Items { get; set; }
    }
}
