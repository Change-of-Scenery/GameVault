namespace GameVault.Models.DTOs;

/// <summary>
/// Data transfer object for creating a new game
/// </summary>
public class CreateGameDto
{
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
