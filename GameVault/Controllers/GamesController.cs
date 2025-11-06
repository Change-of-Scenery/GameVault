using Microsoft.AspNetCore.Mvc;
using GameVault.Models;
using GameVault.Models.DTOs;
using GameVault.Services;

namespace GameVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameRepository _gameRepository;

    /// <summary>
    /// Initializes a new instance of the GamesController
    /// </summary>
    /// <param name="gameRepository">The game repository</param>
    public GamesController(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    /// <summary>
    /// Get all games
    /// </summary>
    /// <returns>A list of all games</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetGames()
    {
        var games = await _gameRepository.GetAllGamesAsync();
        var gameDtos = games.Select(g => MapToDto(g));
        return Ok(gameDtos);
    }

    /// <summary>
    /// Get a specific game by ID
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>The requested game</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> GetGame(int id)
    {
        var game = await _gameRepository.GetGameByIdAsync(id);
        
        if (game == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        return Ok(MapToDto(game));
    }

    /// <summary>
    /// Create a new game
    /// </summary>
    /// <param name="createGameDto">The game to create</param>
    /// <returns>The created game</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GameDto>> CreateGame([FromBody] CreateGameDto createGameDto)
    {
        if (string.IsNullOrWhiteSpace(createGameDto.Title) || string.IsNullOrWhiteSpace(createGameDto.Genre))
        {
            return BadRequest(new { message = "Title and Genre are required fields." });
        }

        var game = new Game
        {
            Title = createGameDto.Title,
            Genre = createGameDto.Genre,
            ReleaseYear = createGameDto.ReleaseYear
        };

        var createdGame = await _gameRepository.CreateGameAsync(game);
        var gameDto = MapToDto(createdGame);
        
        return CreatedAtAction(nameof(GetGame), new { id = gameDto.Id }, gameDto);
    }

    /// <summary>
    /// Update an existing game
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <param name="updateGameDto">The updated game data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGame(int id, [FromBody] UpdateGameDto updateGameDto)
    {
        if (id != updateGameDto.Id)
        {
            return BadRequest(new { message = "ID mismatch between route and body." });
        }

        if (string.IsNullOrWhiteSpace(updateGameDto.Title) || string.IsNullOrWhiteSpace(updateGameDto.Genre))
        {
            return BadRequest(new { message = "Title and Genre are required fields." });
        }

        var game = new Game
        {
            Id = updateGameDto.Id,
            Title = updateGameDto.Title,
            Genre = updateGameDto.Genre,
            ReleaseYear = updateGameDto.ReleaseYear
        };

        var success = await _gameRepository.UpdateGameAsync(game);
        
        if (!success)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        return NoContent();
    }

    /// <summary>
    /// Delete a game
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var success = await _gameRepository.DeleteGameAsync(id);
        
        if (!success)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        return NoContent();
    }

    /// <summary>
    /// Maps a Game entity to a GameDto
    /// </summary>
    /// <param name="game">The game entity</param>
    /// <returns>The game DTO</returns>
    private static GameDto MapToDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Genre = game.Genre,
            ReleaseYear = game.ReleaseYear
        };
    }
}
