using ApiNetCoreHospitales.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiNetCoreHospitales.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext
            (DbContextOptions<HospitalContext> options)
            : base(options) { }

        public DbSet<Hospital> Hospitales { get; set; }
    }
}
