using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Squabby.Helpers.Config;
using Squabby.Models;

namespace Squabby.Database
{
    public class SquabbyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                .EnableSensitiveDataLogging()  
                .UseMySQL(ConfigHelper.GetConfig().ConnectionString);

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasMany(x => x.Boards)
                .WithOne(x => x.Owner);
        }
    }
}