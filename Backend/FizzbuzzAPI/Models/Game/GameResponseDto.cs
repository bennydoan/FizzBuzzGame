﻿//// This is what the backend sends back to the frontend after creating a game

namespace FizzbuzzAPI.Models.Game
{
    public class GameResponseDto
    {
        public int Id { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int TimeDuration { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public List<GameRuleDto> Rules { get; set; } = new List<GameRuleDto>();
        public DateTime CreatedAt { get; set; }
    }
}
