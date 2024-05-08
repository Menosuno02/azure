using System.Data.SqlClient;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace grupofuncioneslunes
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("functionlikempleado")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // VAMOS A PEDIR UN PA´RAMETRO
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);

            string idempleado = query["idempleado"];
            if (idempleado == null)
            {
                var responseBad = req.CreateResponse(HttpStatusCode.BadRequest);
                responseBad.WriteString("Debe proporcionar un ID de empleado");
                return responseBad;
            }

            _logger.LogInformation("Empleado " + idempleado);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            // CADENA DE CONEXIÓN
            string connectionString = Environment.GetEnvironmentVariable("SqlAzure");
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            string sqlUpdate = "UPDATE EMP SET SALARIO = SALARIO + 1 WHERE EMP_NO = " + idempleado;
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sqlUpdate;
            cn.Open();
            com.ExecuteNonQuery();
            string sqlSelect = "SELECT * FROM EMP WHERE EMP_NO = " + idempleado;
            com.CommandText = sqlSelect;
            SqlDataReader reader = com.ExecuteReader();
            string mensaje = "";
            if (reader.Read())
            {
                mensaje = "El empleado " + reader["APELLIDO"].ToString() + " con oficio " + reader["OFICIO"].ToString() + " ha incrementado su salario a " + reader["SALARIO"].ToString();
                reader.Close();
            }
            else
            {
                mensaje = "No existe el empelado con ID " + idempleado;
            }
            cn.Close();
            response.WriteString(mensaje);
            return response;
        }
    }
}
