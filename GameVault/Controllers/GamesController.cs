using Microsoft.AspNetCore.Mvc;
using GameVault.Models;

namespace GameVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    /// <summary>
    /// Static in-memory list of games for testing purposes
    /// </summary>
    private static readonly List<Game> _games = new()
    {
        new Game { Id = 1, Title = "The Legend of Zelda", Genre = "Adventure", ReleaseYear = 1986 },
        new Game { Id = 2, Title = "Super Mario Bros", Genre = "Platformer", ReleaseYear = 1985 },
        new Game { Id = 3, Title = "Minecraft", Genre = "Sandbox", ReleaseYear = 2011 },
        new Game { Id = 4, Title = "The Witcher 3", Genre = "RPG", ReleaseYear = 2015 },
        new Game { Id = 5, Title = "Portal 2", Genre = "Puzzle", ReleaseYear = 2011 }
    };

    /// <summary>
    /// Get all games
    /// </summary>
    /// <returns>A list of all games</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Game>> GetGames()
    {
        return Ok(_games);
    }

    /// <summary>
    /// Get a specific game by ID
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>The requested game</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Game> GetGame(int id)
    {
        var game = _games.FirstOrDefault(g => g.Id == id);
        
        if (game == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        return Ok(game);
    }

    /// <summary>
    /// Create a new game
    /// </summary>
    /// <param name="game">The game to create</param>
    /// <returns>The created game</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Game> CreateGame([FromBody] Game game)
    {
        if (string.IsNullOrWhiteSpace(game.Title) || string.IsNullOrWhiteSpace(game.Genre))
        {
            return BadRequest(new { message = "Title and Genre are required fields." });
        }

        // Generate new ID
        game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
        
        _games.Add(game);
        
        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    /// <summary>
    /// Update an existing game
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <param name="game">The updated game data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateGame(int id, [FromBody] Game game)
    {
        if (string.IsNullOrWhiteSpace(game.Title) || string.IsNullOrWhiteSpace(game.Genre))
        {
            return BadRequest(new { message = "Title and Genre are required fields." });
        }

        var existingGame = _games.FirstOrDefault(g => g.Id == id);
        
        if (existingGame == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        // Update properties
        existingGame.Title = game.Title;
        existingGame.Genre = game.Genre;
        existingGame.ReleaseYear = game.ReleaseYear;
        
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
    public IActionResult DeleteGame(int id)
    {
        var game = _games.FirstOrDefault(g => g.Id == id);
        
        if (game == null)
        {
            return NotFound(new { message = $"Game with ID {id} not found." });
        }
        
        _games.Remove(game);
        
        return NoContent();
    }
}
