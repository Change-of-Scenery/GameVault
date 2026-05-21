using GameVault.Data;
using GameVault.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Services
    {
    /// <summary>
    /// Repository implementation for Game entity operations
    /// </summary>
    public class GameRepository : IGameRepository
        {
        private readonly GameVaultDbContext _context;

        /// <summary>
        /// Initializes a new instance of the GameRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public GameRepository(GameVaultDbContext context)
            {
            _context = context;
            }

        /// <summary>
        /// Gets all games from the database
        /// </summary>
        /// <returns>A collection of all games</returns>
        public async Task<IEnumerable<Game>> GetAllGamesAsync()
            {
            return await _context.Games.ToListAsync();
            }

        /// <summary>
        /// Gets a specific game by ID
        /// </summary>
        /// <param name="id">The game ID</param>
        /// <returns>The game if found, null otherwise</returns>
        public async Task<Game?> GetGameByIdAsync(int id)
            {
            return await _context.Games.FindAsync(id);
            }

        /// <summary>
        /// Creates a new game in the database
        /// </summary>
        /// <param name="game">The game to create</param>
        /// <returns>The created game with generated ID</returns>
        public async Task<Game> CreateGameAsync(Game game)
            {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
            }

        /// <summary>
        /// Updates an existing game in the database
        /// </summary>
        /// <param name="game">The game with updated data</param>
        /// <returns>True if update was successful, false otherwise</returns>
        public async Task<bool> UpdateGameAsync(Game game)
            {
            _context.Entry(game).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                return true;
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!await GameExistsAsync(game.Id))
                    {
                    return false;
                    }
                throw;
                }
            }

        /// <summary>
        /// Deletes a game from the database
        /// </summary>
        /// <param name="id">The ID of the game to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        public async Task<bool> DeleteGameAsync(int id)
            {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
                {
                return false;
                }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return true;
            }

        /// <summary>
        /// Checks if a game exists in the database
        /// </summary>
        /// <param name="id">The game ID</param>
        /// <returns>True if game exists, false otherwise</returns>
        public async Task<bool> GameExistsAsync(int id)
            {
            return await _context.Games.AnyAsync(e => e.Id == id);
            }
        }
    }
