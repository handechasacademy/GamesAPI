using Microsoft.EntityFrameworkCore;
using GamesAPI.Models;

namespace GamesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<GameLibrary> GameLibraries { get; set; }

    }
}
