using Microsoft.EntityFrameworkCore;
using Squabby.Helpers.Config;
using Squabby.Models;

namespace Squabby.Database
{
    public class SquabbyContext : DbContext
    {
        public DbSet<User> Accounts { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseMySQL(ConfigHelper.GetConfig().ConnectionString);

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasMany(x => x.Boards)
                .WithOne(x => x.Owner);

            mb.Entity<Board>()
                .HasMany(x => x.PinnedThreads)
                .WithOne(x => x.Board);
        }
    }
}