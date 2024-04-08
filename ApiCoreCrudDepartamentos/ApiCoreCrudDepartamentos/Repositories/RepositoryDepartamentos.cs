using ApiCoreCrudDepartamentos.Data;
using ApiCoreCrudDepartamentos.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace ApiCoreCrudDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
        private DepartamentosContext context;

        public RepositoryDepartamentos(DepartamentosContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>>
            GetDepartamentosAsync()
        {
            return await this.context.Departamentos.ToListAsync();
        }

        public async Task<Departamento>
            FindDepartamentoAsync(int iddepartamento)
        {
            return await this.context.Departamentos
                .FirstOrDefaultAsync(d => d.DeptNo == iddepartamento);
        }

        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            Departamento departamento = new Departamento();
            departamento.DeptNo = id;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            this.context.Departamentos.Add(departamento);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            Departamento departamento = await this.FindDepartamentoAsync(id);
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            Departamento depart = await this.FindDepartamentoAsync(id);
            this.context.Departamentos.Remove(depart);
            await this.context.SaveChangesAsync();
        }
    }
}
