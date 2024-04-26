using ApiAzureCubos.Helpers;
using ApiAzureCubos.Models;
using ApiAzureCubos.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiAzureCubos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryCubos repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            UsuarioCubo usuario = await this.repo.LoginUsuarioAsync(model);
            if (usuario == null)
                return Unauthorized();
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                usuario.Imagen = "https://storageaccountexamen.blob.core.windows.net/usuarios/" + usuario.Imagen;
                string jsonUsuario = JsonConvert.SerializeObject(usuario);
                Claim[] infoUsuario = new Claim[]
                {
                    new Claim("UserData", jsonUsuario)
                };
                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: infoUsuario,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );
                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Register(UsuarioCubo usuario)
        {
            await this.repo.CreateUsuarioAsync(usuario);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult<UsuarioCubo>> PerfilUsuario()
        {
            string jsonUsuario = HttpContext.User.FindFirst(x => x.Type == "UserData").Value;
            return JsonConvert.DeserializeObject<UsuarioCubo>(jsonUsuario); ;
        }
    }
}

