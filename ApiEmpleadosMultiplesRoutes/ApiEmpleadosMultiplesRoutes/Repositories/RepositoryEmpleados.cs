using ApiEmpleadosMultiplesRoutes.Data;
using ApiEmpleadosMultiplesRoutes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpleadosMultiplesRoutes.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>>
            GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }

        public async Task<Empleado>
            FindEmpleadoAsync(int idempleado)
        {
            return await this.context.Empleados
                .FirstOrDefaultAsync(e => e.IdEmpleado == idempleado);
        }

        public async Task<List<Empleado>>
            GetEmpleadosOficioAsync(string oficio)
        {
            return await this.context.Empleados
                .Where(e => e.Oficio == oficio).ToListAsync();
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            return await this.context.Empleados
                .Select(e => e.Oficio).Distinct().ToListAsync();
            var consulta = (from datos in this.context.Empleados
                            select datos.Oficio).Distinct();
            return await consulta.ToListAsync();
        }

        public async Task<List<Empleado>>
            GetEmpleadosSalarioAsync(int salario, int iddepartamento)
        {
            return await this.context.Empleados
                .Where(e => e.IdDepartamento == iddepartamento
                && e.Salario >= salario).ToListAsync();
        }
    }
}
