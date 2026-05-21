using GameVault.Models;
using Microsoft.EntityFrameworkCore;

namespace GameVault.Data
    {
    /// <summary>
    /// Database context for GameVault application
    /// </summary>
    public class GameVaultDbContext : DbContext
        {
        /// <summary>
        /// Initializes a new instance of the GameVaultDbContext
        /// </summary>
        /// <param name="options">The options to be used by the DbContext</param>
        public GameVaultDbContext(DbContextOptions<GameVaultDbContext> options) : base(options)
            {
            }

        /// <summary>
        /// Gets or sets the Games DbSet
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Configures the model and seeds initial data
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Title = "The Legend of Zelda", Genre = "Adventure", ReleaseYear = 1986 },
                new Game { Id = 2, Title = "Super Mario Bros", Genre = "Platformer", ReleaseYear = 1985 },
                new Game { Id = 3, Title = "Minecraft", Genre = "Sandbox", ReleaseYear = 2011 },
                new Game { Id = 4, Title = "The Witcher 3", Genre = "RPG", ReleaseYear = 2015 },
                new Game { Id = 5, Title = "Portal 2", Genre = "Puzzle", ReleaseYear = 2011 }
            );
            }
        }
    }
