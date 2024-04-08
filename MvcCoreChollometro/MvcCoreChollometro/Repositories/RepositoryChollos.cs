using Microsoft.EntityFrameworkCore;
using MvcCoreChollometro.Data;
using MvcCoreChollometro.Models;

namespace MvcCoreChollometro.Repositories
{
    public class RepositoryChollos
    {
        private ChollosContext context;

        public RepositoryChollos(ChollosContext context)
        {
            this.context = context;
        }

        public async Task<List<Chollo>> GetChollosAsync()
        {
            return await this.context.Chollos
                .OrderByDescending(c => c.Fecha)
                .ToListAsync();
        }
    }
}
