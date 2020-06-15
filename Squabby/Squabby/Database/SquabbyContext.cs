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
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }))
                .EnableSensitiveDataLogging()
                .UseMySQL(ConfigHelper.GetConfig().ConnectionString);

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Thread>()
                .HasOne(x => x.Board)
                .WithMany(x => x.Threads)
                .HasForeignKey(x => x.BoardId);
            mb.Entity<Thread>().HasKey(x => new {x.Id, x.BoardId});
            
            mb.Entity<Comment>()
                .HasOne(x => x.Thread)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => new {x.BoardId, x.ThreadId});
            mb.Entity<Comment>().HasKey(x => new {x.Id, x.BoardId, x.ThreadId});

            mb.Entity<Rating>()
                .HasOne(x => x.Thread)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => new {x.ThreadId, x.BoardId});
            mb.Entity<Rating>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId);
            mb.Entity<Rating>().HasKey(x => new {x.UserId, x.BoardId, x.ThreadId});
        }
    }
}