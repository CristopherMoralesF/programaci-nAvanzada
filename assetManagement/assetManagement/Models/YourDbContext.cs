using Microsoft.EntityFrameworkCore;
using assetManagementClassLibrary.Models;
using assetManagement.Models;

namespace assetManagement.Models
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
        {
        }

        // DbSet for UsuariosEnt entity
        public DbSet<UsuariosEnt> Usuarios { get; set; }

        // Other DbSets or configurations can be added here for other entities

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuariosEnt>().HasKey(u => u.ID_USUARIO);

        }
    }
}
