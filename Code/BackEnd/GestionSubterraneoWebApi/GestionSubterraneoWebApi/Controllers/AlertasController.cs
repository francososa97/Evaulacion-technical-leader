using GestionSubterraneoWebApi.Data;
using GestionSubterraneoWebApi.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Alertas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Controllers
{
    /// <summary>
    /// este controlador Gestiona las llamadas y peticiones con respecto a la api /subtes/serviceAlerts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AlertasController : ControllerBase
    {

        #region Atributos privados

        /// <summary>
        /// Atributo privado que almacena el modelo de enrity framework core, para poder comunicarse con la base de datos
        /// </summary>
        private readonly RegistroIncidenteDbContext _context;
        /// <summary>
        /// Atributo privado en el que se almacena el modelo de respuesta de /subtes/serviceAlerts
        /// </summary>
        private ResponseAlerta informacionAlerta;


        #endregion

        #region Metodos endpoints

        /// <summary>
        /// Este metodo en capsula la informacion de todos los registros que se hicieron por consultar los registros de incidentes, la informacion proviene de la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<RegistroIncidente>> GetRegistrosIncidentes()
        {
            var response = new List<RegistroIncidente>();
            using (var context = new RegistroIncidenteDbContext())
            {
                response = await context.Registros.ToListAsync();
            }
            return response;
        }

        /// <summary>
        /// Este metodo devuelve un unico registro de incidentes almacenado en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistrosIncidentesByIdAsync(int id)
        {
            var RegistroSeleccionado = new RegistroIncidente();
            using (var context = new RegistroIncidenteDbContext())
            {
                var response = await context.Registros.ToListAsync();
                RegistroSeleccionado = response.Where(Registro => Registro.Id == id).ToList().FirstOrDefault();
            }

            if (RegistroSeleccionado == null)
            {
                var mensajeError = NotFound("El registro de incidente con id: " + id.ToString() + " no existe.");
                return mensajeError;
            }

            return Ok(RegistroSeleccionado);
        }

        /// <summary>
        /// Metodo que encapsula la llamada a /subtes/serviceAlerts y lo mapea a un modelo de abstraccion llamado ResponseAlertaAbstraccion(con el fin de proporcionar informacion mas simplificada a la spa)
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/alertaestado/{nombreUsuario}")]
        public IActionResult GetEstadoRamales(string nombreUsuario)
        {
            var nombreCifrado = GestionSubterraneoWebApi.Helper.GestionSubterraneoHelper.ObtenerClienteId(nombreUsuario);
            this.informacionAlerta = GetAlerta(nombreCifrado).Result;
            var response = GestionarInformacionResponse(_context, this.informacionAlerta, nombreUsuario);
            return Ok(response);
        }

        /// <summary>
        /// Metodo que encapsula la llamada a ApiMockSubterraneo(revisar en evaluaciontecnica/code/backend ) y lo mapea a un modelo de abstraccion llamado ResponseAlertaAbstraccion(con el fin de proporcionar informacion mas simplificada a la spa)
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/mock/alertaestado/{nombreUsuario}")]
        public IActionResult GetEstadoRamalesTest(string nombreUsuario)
        {
            var nombreCifrado = GestionSubterraneoWebApi.Helper.GestionSubterraneoHelper.ObtenerClienteId(nombreUsuario);
            this.informacionAlerta = GetAlertaMock(nombreCifrado).Result;
            var response = GestionarInformacionResponse(_context, this.informacionAlerta, nombreUsuario);
            return Ok(response);
        }

        #endregion

        #region Metodos Privados
        /// <summary>
        /// Metodo privado que encapsula la informacion de la llamada a la api /subtes/serviceAlerts y retorna un modelo abstraido de la llamada
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<ResponseAlerta> GetAlerta(string clientId)
        {
            ResponseAlerta ForecastCliente = new ResponseAlerta();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string clientSecret = "1";
                    var endPoint = $"https://apitransporte.buenosaires.gob.ar/subtes/serviceAlerts?json=1&client_id={clientId}&client_secret={clientSecret}";
                    client.BaseAddress = new Uri(endPoint);
                    client.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await client.GetAsync(endPoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        ForecastCliente = JsonConvert.DeserializeObject<ResponseAlerta>(responseJson);
                    }
                }

                return ForecastCliente;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        /// <summary>
        /// Metodo privado que encapsula la informacion de la llamada a la api  ApiMockSubterraneo(revisar en evaluaciontecnica/code/backend ) y retorna un modelo abstraido de la llamada
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<ResponseAlerta> GetAlertaMock(string clientId)
        {
            ResponseAlerta ForecastCliente = new ResponseAlerta();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("client_id", clientId);
                    var endPoint = $"https://localhost:44302/ApiMockAlerts";
                    client.BaseAddress = new Uri(endPoint);
                    client.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await client.GetAsync(endPoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        ForecastCliente = JsonConvert.DeserializeObject<ResponseAlerta>(responseJson);
                    }
                }

                return ForecastCliente;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        /// <summary>
        /// Metodo que encapasula la logica para obtener un response mas simplificado en un modelo abstraido y luego registra la informacion en la base de datos
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="informacionAlerta"></param>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        private ResponseAlertaAbstraccion GestionarInformacionResponse(RegistroIncidenteDbContext contexto, ResponseAlerta informacionAlerta, string nombreUsuario)
        {
            var responseAbstraido = MapResponseAlert(informacionAlerta);

            GrabarRegistroIncidente(responseAbstraido, nombreUsuario);
            foreach (var periodoIncidente in responseAbstraido.PeriodoIncidentes)
            {
                var incideteRegistrar = new RegistroIncidente(0, periodoIncidente.InicioIncidente, periodoIncidente.FinIncidente, DateTime.Now, nombreUsuario, responseAbstraido.TipoCausa, responseAbstraido.TipoEfecto);
            }
            return responseAbstraido;
        }

        /// <summary>
        /// Metodo por el cual se hace la incercion con el modelo RegistroIncidente  a la base de datos por medio de entity framework core
        /// </summary>
        /// <param name="nuevoRegistroIncidente"></param>
        private void GrabarRegistroIncidente(ResponseAlertaAbstraccion nuevoRegistroIncidente, string nombreUsuario)
        {
            var responseAlertaServico = informacionAlerta.AlertaRamal.FirstOrDefault();
            var descripcionDemora = responseAlertaServico.AlertaIncidente.MotivosDemoras.TraduccionDemora.Where(x => x.Lenguaje == "es").FirstOrDefault().Descripcion;
            var ramalAfectado = descripcionDemora.FirstOrDefault().ToString();
            foreach (var periodoIncidente in nuevoRegistroIncidente.PeriodoIncidentes)
            {
                using (var context = new RegistroIncidenteDbContext())
                {
                    var incideteRegistrar = new RegistroIncidente(0, periodoIncidente.InicioIncidente, periodoIncidente.FinIncidente, DateTime.Now, nombreUsuario, nuevoRegistroIncidente.TipoCausa, nuevoRegistroIncidente.TipoEfecto);
                    context.Registros.Add(incideteRegistrar);
                    context.SaveChanges();

                }
            }
        }

        /// <summary>
        /// Metodo que convierte un objeto de modelo ResponseAlerta a un objeto con el modelo ResponseAlertaAbstraccion
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private ResponseAlertaAbstraccion MapResponseAlert(ResponseAlerta response)
        {
            var responseAlertaServico = response.AlertaRamal.FirstOrDefault();
            var descripcionDemora = responseAlertaServico.AlertaIncidente.MotivosDemoras.TraduccionDemora.Where(x => x.Lenguaje == "es").FirstOrDefault().Descripcion;
            var ramalAfectado = responseAlertaServico.AlertaIncidente.InformacionRamal.FirstOrDefault().IdRuta.Last().ToString();
            var responseAbs = new ResponseAlertaAbstraccion((TipoCausa)responseAlertaServico.AlertaIncidente.Causa, (TipoEfecto)responseAlertaServico.AlertaIncidente.Efecto, descripcionDemora, ramalAfectado);

            foreach (var periodoDemora in responseAlertaServico.AlertaIncidente.FechasPeriodos)
            {
                var fechaInicio = TimeStampADateTime(periodoDemora.FechaInicio);
                var fechaFin = TimeStampADateTime(periodoDemora.FechaFin);
                var duracionDemora = new DuracionIncidente(fechaInicio, fechaFin);
                responseAbs.PeriodoIncidentes.Add(duracionDemora);
            }
            return responseAbs;
        }

        /// <summary>
        /// Metodo que convierte la fecha de unixTimeStamp (porveniente de response /subtes/serviceAlerts ) a DateTime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        private static DateTime TimeStampADateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        #endregion
    }
}
