using ApiCoreOAuthEmpleados.Helpers;
using ApiCoreOAuthEmpleados.Models;
using ApiCoreOAuthEmpleados.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiCoreOAuthEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryEmpleados repo;

        // Cuando generemos el token debemos integrar
        // dentro de dicho token, Issuer, Audience...
        // Para quue lo valide cuando nos lo envien
        private HelperActionServicesOAuth helper;

        public AuthController
            (RepositoryEmpleados repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        // Necesitamos un método POST para validar el
        // usuario y que recibirá LoginModel
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login
            (LoginModel model)
        {
            // Buscamos al empleado en nuestro repo
            Empleado empleado = await this.repo.LogInEmpleadoAsync(model.Username, int.Parse(model.Password));
            if (empleado == null)
            {
                return Unauthorized();
            }
            else
            {
                // Debemos crear unas credenciales
                // para incluirlas dentro del token
                // y que estarán compuestas por el
                // secret key cifrado y el tipo de
                // cifrado que deseemos incluir en
                // el token
                SigningCredentials credentials =
                    new SigningCredentials(helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);

                // Convertimos el empleado a JSON
                string jsonEmpleado =
                    JsonConvert.SerializeObject(empleado);
                // Creamos un array de claims con toda la
                // información que deseamos guardar en el token
                Claim[] informacion = new Claim[]
                {
                    new Claim("UserData", jsonEmpleado)
                };

                // El token se genera con una clase y
                // debemos indicar los elementos que
                // almacenará dentro de dicho token
                // Como Issuer, Audience o el tiempo
                // de validación del Token
                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: informacion,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );
                // Por último devolvemos una respuesta
                // afirmativa con un objeto anónimo en
                // formato JSON
                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
        }
    }
}
