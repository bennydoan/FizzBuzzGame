using FizzbuzzAPI.Models.Game;
using Microsoft.EntityFrameworkCore;

namespace FizzbuzzAPI.Data
{
    public class FizzBuzzDBContext : DbContext
    {
        public FizzBuzzDBContext(DbContextOptions <FizzBuzzDBContext> options) : base(options)
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameRule> GameRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Game and GameRule
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Rules)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
