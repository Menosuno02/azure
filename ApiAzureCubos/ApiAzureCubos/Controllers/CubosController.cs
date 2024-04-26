using ApiAzureCubos.Models;
using ApiAzureCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiAzureCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;

        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }

        [HttpGet]
        [Route("CubosMarca/{marca}")]
        public async Task<ActionResult<List<Cubo>>> GetCubosMarca(string marca)
        {
            return await this.repo.GetCubosMarcaAsync(marca);
        }

        [HttpGet]
        [Authorize]
        [Route("PedidosUsuario")]
        public async Task<ActionResult<List<CompraCubo>>> GetComprasUsuario()
        {
            string jsonUsuario = HttpContext.User.FindFirst(x => x.Type == "UserData").Value;
            UsuarioCubo usu = JsonConvert.DeserializeObject<UsuarioCubo>(jsonUsuario);
            return await this.repo.GetComprasUsuarioAsync(usu.IdUsuario);
        }

        [HttpPost]
        [Authorize]
        [Route("RealizarPedido/{idcubo}")]
        public async Task<ActionResult> CreateCompraCubo(int idcubo)
        {
            string jsonUsuario = HttpContext.User.FindFirst(x => x.Type == "UserData").Value;
            UsuarioCubo usu = JsonConvert.DeserializeObject<UsuarioCubo>(jsonUsuario);
            await this.repo.CreateCompraCubo(usu.IdUsuario, idcubo);
            return Ok();
        }
    }
}
