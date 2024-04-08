using ApiNetCoreHospitales.Models;
using ApiNetCoreHospitales.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNetCoreHospitales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalesController : ControllerBase
    {
        private RepositoryHospitales repo;

        public HospitalesController(RepositoryHospitales repo)
        {
            this.repo = repo;
        }

        // Los nombres de métodos en los métodos por defecto
        // no importan. Aun así, seguimos las mismas normas de
        // no incluir la palabra async dentro del metodo de
        // un controller

        [HttpGet]
        public async Task<ActionResult<List<Hospital>>>
            GetHospitales()
        {
            return await this.repo.GetHospitalesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> FindHospital(int id)
        {
            return await this.repo.FindHospitalAsync(id);
        }
    }
}
