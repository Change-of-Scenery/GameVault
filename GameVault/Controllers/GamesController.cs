using Microsoft.AspNetCore.Mvc;

namespace GameVault.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    /// <summary>
    /// Get all games
    /// </summary>
    /// <returns>A list of all games</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Game>> GetGames()
    {
        var games = new List<Game>
        {
            new Game { Id = 1, Title = "The Legend of Zelda", Genre = "Adventure", ReleaseYear = 1986 },
            new Game { Id = 2, Title = "Super Mario Bros", Genre = "Platformer", ReleaseYear = 1985 },
            new Game { Id = 3, Title = "Minecraft", Genre = "Sandbox", ReleaseYear = 2011 }
        };
        
        return Ok(games);
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
        var game = new Game { Id = id, Title = "Sample Game", Genre = "Action", ReleaseYear = 2024 };
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateGame(int id, [FromBody] Game game)
    {
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
        return NoContent();
    }
}

/// <summary>
/// Represents a game in the vault
/// </summary>
public class Game
{
    /// <summary>
    /// The unique identifier for the game
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The title of the game
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// The genre of the game
    /// </summary>
    public required string Genre { get; set; }
    
    /// <summary>
    /// The year the game was released
    /// </summary>
    public int ReleaseYear { get; set; }
}
