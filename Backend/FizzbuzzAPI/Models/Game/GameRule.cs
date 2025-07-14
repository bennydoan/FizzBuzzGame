using System.ComponentModel.DataAnnotations;

namespace FizzbuzzAPI.Models.Game
{
    public class GameRule
    {
        [Key]
        public int Id { get; set; }
        
        public int GameId { get; set; }
        
        [Required(ErrorMessage = "Divisor cannot be null")]
        public int Divisor { get; set; }
        
        [Required(ErrorMessage = "Replacement cannot be null")]
        public string Replacement { get; set; } = string.Empty;
        
        // Navigation property
        public Game Game { get; set; } = null!;
    }
} 