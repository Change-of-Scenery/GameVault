using GameVault.Models;

namespace GameVault.Services;

/// <summary>
/// Repository interface for Game entity operations
/// </summary>
public interface IGameRepository
{
    /// <summary>
    /// Gets all games from the database
    /// </summary>
    /// <returns>A collection of all games</returns>
    Task<IEnumerable<Game>> GetAllGamesAsync();

    /// <summary>
    /// Gets a specific game by ID
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>The game if found, null otherwise</returns>
    Task<Game?> GetGameByIdAsync(int id);

    /// <summary>
    /// Creates a new game in the database
    /// </summary>
    /// <param name="game">The game to create</param>
    /// <returns>The created game with generated ID</returns>
    Task<Game> CreateGameAsync(Game game);

    /// <summary>
    /// Updates an existing game in the database
    /// </summary>
    /// <param name="game">The game with updated data</param>
    /// <returns>True if update was successful, false otherwise</returns>
    Task<bool> UpdateGameAsync(Game game);

    /// <summary>
    /// Deletes a game from the database
    /// </summary>
    /// <param name="id">The ID of the game to delete</param>
    /// <returns>True if deletion was successful, false otherwise</returns>
    Task<bool> DeleteGameAsync(int id);

    /// <summary>
    /// Checks if a game exists in the database
    /// </summary>
    /// <param name="id">The game ID</param>
    /// <returns>True if game exists, false otherwise</returns>
    Task<bool> GameExistsAsync(int id);
}
