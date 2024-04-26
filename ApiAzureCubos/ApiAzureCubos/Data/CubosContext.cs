using ApiAzureCubos.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAzureCubos.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options) : base(options) { }

        public DbSet<CompraCubo> ComprasCubo { get; set; }
        public DbSet<Cubo> Cubos { get; set; }
        public DbSet<UsuarioCubo> UsuariosCubo { get; set; }
    }
}
