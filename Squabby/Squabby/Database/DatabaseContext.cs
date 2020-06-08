using Microsoft.EntityFrameworkCore;

namespace Squabby.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=192.168.1.3;database=squabby;user=root;password=root-password;");
        }
    }
}