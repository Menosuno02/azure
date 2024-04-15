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

        // GET: api/Empleados
        /// <summary>
        /// Obtiene el conjunto de empleados, tabla EMP.
        /// </summary>
        /// <remarks>
        /// Método para devolver todos los empleados de la BBDD
        /// </remarks>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response> 
        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetEmpleados()
        {
            return await this.repo.GetEmpleadosAsync();
        }

        /// <summary>
        /// Obtiene una Empleado por su Id, tabla EMP.
        /// </summary>
        /// <remarks>
        /// Permite buscar un objeto Empleado por su ID
        /// </remarks>
        /// <param name="idempleado">Id (GUID) del objeto Empleado.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>>
            FindEmpleado(int idempleado)
        {
            Empleado empleado =
                await this.repo.FindEmpleadoAsync(idempleado);
            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(empleado);
            }
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
