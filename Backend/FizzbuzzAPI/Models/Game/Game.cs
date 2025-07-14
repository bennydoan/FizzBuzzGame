using System.ComponentModel.DataAnnotations;

namespace FizzbuzzAPI.Models.Game
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Game name cannot be null")]
        [StringLength(100)]
        public string GameName { get; set; } = string.Empty;
        
        public string Author { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Time duration cannot be null")]
        public int TimeDuration { get; set; }
        
        [Required(ErrorMessage = "Start number cannot be null")]
        public int Start { get; set; }
        
        [Required(ErrorMessage = "End number cannot be null")]
        public int End { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation property for multiple rules
        public ICollection<GameRule> Rules { get; set; } = new List<GameRule>();
    }
} 