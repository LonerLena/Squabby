using Microsoft.EntityFrameworkCore;
using Squabby.Helpers.Config;

namespace Squabby.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConfigHelper.GetConfig().ConnectionString);
        }
    }
}