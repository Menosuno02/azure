using ApiCoreCrudDepartamentos.Models;
using ApiCoreCrudDepartamentos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreCrudDepartamentos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private RepositoryDepartamentos repo;

        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Departamento>>>
            GetDepartamentos()
        {
            return await this.repo.GetDepartamentosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>>
            FindDepartamento(int id)
        {
            return await this.repo.FindDepartamentoAsync(id);
        }

        // Los métodos por defecto de POST o PUT reciben un objeto
        // Si quisieramos recibir los datos por parámetros,
        // debemos utilizar Route
        // Podemos personalizar la respuesta en el caso que no nos
        // guste algo, pudiendo devolver NotFound, BadRequest
        [HttpPost]
        public async Task<ActionResult> PostDepartamento
            (Departamento departamento)
        {
            await this.repo.InsertDepartamentoAsync(departamento.DeptNo,
              departamento.Nombre, departamento.Localidad);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            // Podemos personalizar la respuesta
            if (await this.repo.FindDepartamentoAsync(id) == null)
            {
                // No existe el departamento
                return NotFound();
            }
            else
            {
                await this.repo.DeleteDepartamentoAsync(id);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutDepartamento
            (Departamento departamento)
        {
            await this.repo.UpdateDepartamentoAsync(departamento.DeptNo,
                departamento.Nombre, departamento.Localidad);
            return Ok();
        }

        // Podemos tener todos los métodos POST/PUT/DELETE
        // que deseemos utilizando Route
        [HttpPost]
        [Route("[action]/{id}/{nombre}/{localidad}")]
        public async Task<ActionResult> InsertParams
            (int id, string nombre, string localidad)
        {
            await this.repo.InsertDepartamentoAsync
                (id, nombre, localidad);
            return Ok();
        }

        // También podemos combinar recibir objetos con Routes
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> UpdateParams
            (int id, Departamento departamento)
        {
            await this.repo.UpdateDepartamentoAsync(id,
                departamento.Nombre, departamento.Localidad);
            return Ok();
        }
    }
}
