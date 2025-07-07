using System.ComponentModel.DataAnnotations;

namespace FizzbuzzAPI.Models.Game
{
    public class CreateGameDto
    {
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

        [Required(ErrorMessage = "Divisor cannot be null")]
        public int Divisor { get; set; }

        [Required(ErrorMessage = "Replacement cannot be null")]
        public string Replacement { get; set; } = string.Empty;
    }
}


// this is the Data Transfer Object (DTO) for creating a game, that send all data frontend ask to backend