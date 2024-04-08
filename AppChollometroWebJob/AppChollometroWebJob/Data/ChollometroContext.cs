using AppChollometroWebJob.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChollometroWebJob.Data
{
    public class ChollometroContext : DbContext
    {
        public ChollometroContext(DbContextOptions<ChollometroContext> options) : base(options) { }

        public DbSet<Chollo> Chollos { get; set; }
    }
}
