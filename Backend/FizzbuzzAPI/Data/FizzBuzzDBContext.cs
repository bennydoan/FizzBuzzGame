using FizzbuzzAPI.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace FizzbuzzAPI.Data
{
    public class FizzBuzzDBContext : DbContext
    {
        public FizzBuzzDBContext(DbContextOptions <FizzBuzzDBContext> options) : base(options)
        {
            
        }
        public DbSet<Game> Games { get; set; } // 
    }
}
