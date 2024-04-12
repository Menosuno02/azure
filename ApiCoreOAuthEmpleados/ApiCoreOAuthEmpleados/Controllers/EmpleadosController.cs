using ApiCoreOAuthEmpleados.Models;
using ApiCoreOAuthEmpleados.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace ApiCoreOAuthEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados()
        {
            return await this.repo.GetEmpleadosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>>
            FindEmpleado(int id)
        {
            return await this.repo.FindEmpleadoAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Empleado>> PerfilEmpleado()
        {
            // Internamente, cuando recibimos el token el usuario
            // es validado y almacena datos como
            // HttpContext.User.Identity.isAuthenticated
            // Como hemos incluido la key de los Claims,
            // automáticamente también tenemos dichos Claims como
            // en las aplicaciones MVC
            Claim claim = HttpContext.User
                .FindFirst(x => x.Type == "UserData");
            // Recuperamos el JSON del empleado
            string jsonEmpleado = claim.Value;
            Empleado empleado = JsonConvert.DeserializeObject<Empleado>(jsonEmpleado);
            return empleado;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Empleado>>>
            CompisCurro()
        {
            string jsonEmpleado =
                HttpContext.User.FindFirst(x => x.Type == "UserData").Value;
            Empleado empleado = JsonConvert.DeserializeObject<Empleado>(jsonEmpleado);
            List<Empleado> compis = await this.repo.GetCompisDepartamento(empleado.IdDepartamento);
            return compis;
        }

        // localhost://api/multiplesdatos?ids=11&ids=74&ids=99
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult>
            MultiplesDatos([FromQuery] List<int> ids)
        {
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<string>>> Oficios()
        {
            return await this.repo.GetOficiosAsync();
        }

        //?oficio=dato&oficio=dato2
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Empleado>>>
            EmpleadosOficios([FromQuery] List<string> oficio)
        {
            return await this.repo.GetEmpleadosOficiosAsync(oficio);
        }

        //localhost/api/Update/25?oficio=Dato
        [HttpPut]
        [Route("[action]/{incremento}")]
        public async Task<ActionResult> IncrementarSalarioOficios
            (int incremento, [FromQuery] List<string> oficio)
        {
            await this.repo.IncrementarSalarioEmpleadosOficiosAsync(incremento, oficio);
            return Ok();
        }
    }
}
