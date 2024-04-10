using ApiCoreOAuthEmpleados.Data;
using ApiCoreOAuthEmpleados.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCoreOAuthEmpleados.Repositories
{
    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados
                .FirstOrDefaultAsync(x => x.IdEmpleado == idEmpleado);
        }

        public async Task<Empleado>
            LogInEmpleadoAsync(string apellido, int idEmpleado)
        {
            return await this.context.Empleados
                .Where(e => e.Apellido == apellido
                && e.IdEmpleado == idEmpleado)
                .FirstOrDefaultAsync();
        }
    }
}
