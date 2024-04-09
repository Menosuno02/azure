using ApiCrudCoreDoctores.Models;
using ApiCrudCoreDoctores.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudCoreDoctores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctores()
        {
            return await this.repo.GetDoctoresAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> FindDoctor(int id)
        {
            return await this.repo.FindDoctorAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor
            (Doctor doctor)
        {
            await this.repo.CreateDoctorAsync(doctor.HospitalCod,
                doctor.DoctorNo, doctor.Apellido, doctor.Especialidad,
                doctor.Salario);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDoctor
            (Doctor doctor)
        {
            await this.repo.UpdateDoctorAsync(doctor.HospitalCod,
                doctor.DoctorNo, doctor.Apellido, doctor.Especialidad,
                doctor.Salario);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await this.repo.FindDoctorAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                await this.repo.DeleteDoctorAsync(id);
                return Ok();
            }
        }
    }
}
