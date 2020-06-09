using Microsoft.EntityFrameworkCore;
using Squabby.Helpers.Config;
using Squabby.Models;

namespace Squabby.Database
{
    public class SquabbyContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConfigHelper.GetConfig().ConnectionString);
        }
    }
}