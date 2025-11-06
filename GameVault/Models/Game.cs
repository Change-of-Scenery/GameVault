namespace GameVault.Models;

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
