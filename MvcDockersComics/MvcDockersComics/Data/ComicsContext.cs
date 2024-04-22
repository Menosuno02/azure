using Microsoft.EntityFrameworkCore;
using MvcDockersComics.Models;

namespace MvcDockersComics.Data
{
    public class ComicsContext : DbContext
    {
        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

        public DbSet<Comic> Comics { get; set; }
    }
}
