using FizzbuzzAPI.Data;
using FizzbuzzAPI.Models.Auth;
using FizzbuzzAPI.Models.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FizzbuzzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] - Temporarily removed for testing
    public class GameCreateController : ControllerBase
    {
        private readonly FizzBuzzDBContext _context;
        public GameCreateController(FizzBuzzDBContext context)
        {

            _context = context;
        }

        // GET: api/GameCreate
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var games = await _context.Games
                    .OrderByDescending(g => g.CreatedAt)
                    .Select(g => new GameResponseDto
                    {
                        Id = g.Id,
                        GameName = g.GameName,
                        Author = g.Author,
                        TimeDuration = g.TimeDuration,
                        Start = g.Start,
                        End = g.End,
                        Divisor = g.Divisor,
                        Replacement = g.Replacement,
                        CreatedAt = g.CreatedAt
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = "Games retrieved successfully",
                    games = games,
                    count = games.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving games", error = ex.Message });
            }
        }

        // GET: api/GameCreate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);

                if (game == null)
                {
                    return NotFound(new { message = "Game not found" });
                }

                var response = new GameResponseDto
                {
                    Id = game.Id,
                    GameName = game.GameName,
                    Author = game.Author,
                    TimeDuration = game.TimeDuration,
                    Start = game.Start,
                    End = game.End,
                    Divisor = game.Divisor,
                    Replacement = game.Replacement,
                    CreatedAt = game.CreatedAt
                };

                return Ok(new
                {
                    message = "Game retrieved successfully",
                    game = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the game", error = ex.Message });
            }
        }

        // GET: api/GameCreate/author/{authorName}
        [HttpGet("author/{authorName}")]
        public async Task<IActionResult> GetGamesByAuthor(string authorName)
        {
            try
            {
                var games = await _context.Games
                    .Where(g => g.Author == authorName)
                    .OrderByDescending(g => g.CreatedAt)
                    .Select(g => new GameResponseDto
                    {
                        Id = g.Id,
                        GameName = g.GameName,
                        Author = g.Author,
                        TimeDuration = g.TimeDuration,
                        Start = g.Start,
                        End = g.End,
                        Divisor = g.Divisor,
                        Replacement = g.Replacement,
                        CreatedAt = g.CreatedAt
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = "Games retrieved successfully",
                    games = games,
                    count = games.Count,
                    author = authorName
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving games", error = ex.Message });
            }
        }

        // GET: api/GameCreate/my-games
        [HttpGet("my-games")]
        public async Task<IActionResult> GetMyGames()
        {
            try
            {
                // Get the current user's username from the JWT token
                var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest(new { message = "User not authenticated properly" });
                }

                var games = await _context.Games
                    .Where(g => g.Author == username)
                    .OrderByDescending(g => g.CreatedAt)
                    .Select(g => new GameResponseDto
                    {
                        Id = g.Id,
                        GameName = g.GameName,
                        Author = g.Author,
                        TimeDuration = g.TimeDuration,
                        Start = g.Start,
                        End = g.End,
                        Divisor = g.Divisor,
                        Replacement = g.Replacement,
                        CreatedAt = g.CreatedAt
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = "Your games retrieved successfully",
                    games = games,
                    count = games.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving your games", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var game = new Game
                {
                    GameName = gameDto.GameName,
                    Author = gameDto.Author,
                    TimeDuration = gameDto.TimeDuration,
                    Start = gameDto.Start,
                    End = gameDto.End,
                    Divisor = gameDto.Divisor,
                    Replacement = gameDto.Replacement,

                };

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                var response = new GameResponseDto
                {
                    Id = game.Id,
                    GameName = game.GameName,
                    Author = game.Author,
                    TimeDuration = game.TimeDuration,
                    Start = game.Start,
                    End = game.End,
                    Divisor = game.Divisor,
                    Replacement = game.Replacement,
                    CreatedAt = DateTime.UtcNow
                };
                return Ok(new
                {
                    message = "Game created successfully",
                    game = response
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the game", error = ex.Message });
            }
        }
    }
}
