using ApiNetCoreHospitales.Data;
using ApiNetCoreHospitales.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiNetCoreHospitales.Repositories
{
    public class RepositoryHospitales
    {
        private HospitalContext context;

        public RepositoryHospitales(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Hospital>> GetHospitalesAsync()
        {
            return await this.context.Hospitales.ToListAsync();
        }

        public async Task<Hospital> FindHospitalAsync(int idhospital)
        {
            return await this.context.Hospitales
                .FirstOrDefaultAsync(h => h.IdHospital == idhospital);
        }
    }
}
