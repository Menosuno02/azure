using ApiCrudCoreDoctores.Data;
using ApiCrudCoreDoctores.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCrudCoreDoctores.Repositories
{
    public class RepositoryDoctores
    {
        private DoctoresContext context;

        public RepositoryDoctores(DoctoresContext context)
        {
            this.context = context;
        }

        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            return await this.context.Doctores.ToListAsync();
        }

        public async Task<Doctor> FindDoctorAsync(int doctorNo)
        {
            return await this.context.Doctores
                .FirstOrDefaultAsync(d => d.DoctorNo == doctorNo);
        }

        public async Task CreateDoctorAsync
            (int hospitalCod, int doctorNo, string apellido,
            string especialidad, int salario)
        {
            Doctor doctor = new Doctor
            {
                HospitalCod = hospitalCod,
                DoctorNo = doctorNo,
                Apellido = apellido,
                Especialidad = especialidad,
                Salario = salario
            };
            this.context.Doctores.Add(doctor);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync
            (int hospitalCod, int doctorNo, string apellido,
            string especialidad, int salario)
        {
            Doctor doctor = await this.FindDoctorAsync(doctorNo);
            doctor.HospitalCod = hospitalCod;
            doctor.Apellido = apellido;
            doctor.Especialidad = especialidad;
            doctor.Salario = salario;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int doctorNo)
        {
            Doctor doctor = await this.FindDoctorAsync(doctorNo);
            this.context.Doctores.Remove(doctor);
            await this.context.SaveChangesAsync();
        }
    }
}
