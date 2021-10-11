using GestionSubterraneoWebApi.Data;
using GestionSubterraneoWebApi.Modelos;
using Microsoft.AspNetCore.Http;
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
    [ApiController]
    [Route("[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly RegistroIncidenteDbContext _context;
        private ResponseAlerta informacionAlerta;
        public AlertasController(RegistroIncidenteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<RegistroIncidente>> GetRegistrosIncidentes() => await _context.Registros.ToListAsync();

        [HttpGet("{id}")]
        public IActionResult GetRegistrosIncidentesById(int id)
        {
            var RegistroSeleccionado = _context.Registros.Where(Registro => Registro.Id == id).ToList().FirstOrDefault();

            if(RegistroSeleccionado == null)
            {
                var mensajeError = NotFound("El registro de incidente con id:" + id.ToString() + " no existe.");
                return mensajeError;
            }

            return Ok(RegistroSeleccionado);
        }

        [HttpGet]
        [Route("alertaestado")]
        public IActionResult GetEstadoRamales()
        {
            var response = GetAlerta().Result;
            return Ok(response);
        }

        [HttpGet]
        [Route("/mock/alertaestado")]
        public IActionResult GetEstadoRamalesTest()
        {
            var response = GetAlertaMock().Result;
            return Ok(response);
        }
        [HttpGet]
        [Route("/history/incidentes/{nombreUsuario}")]
        public IActionResult GetHistorialIncidentes(string nombreUsuario)
        {
            if(this.informacionAlerta == null)
            {
                this.informacionAlerta = GetAlertaMock().Result;
            }
            var historialIncidentes = AgregarRegistroIncidente(_context, this.informacionAlerta,nombreUsuario);
            return Ok(historialIncidentes);
        }

        #region Metodos Privados

        private async Task<ResponseAlerta> GetAlerta()
        {
            ResponseAlerta ForecastCliente = new ResponseAlerta();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string idClient = "1";
                    string clientSecret = "1";
                    var endPoint = $"https://apitransporte.buenosaires.gob.ar/subtes/serviceAlerts?json=1&client_id={idClient}&client_secret={clientSecret}";
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
        private async Task<ResponseAlerta> GetAlertaMock()
        {
            ResponseAlerta ForecastCliente = new ResponseAlerta();

            try
            {
                using (HttpClient client = new HttpClient())
                {
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

        private List<DuracionIncidente> AgregarRegistroIncidente(RegistroIncidenteDbContext contexto, ResponseAlerta informacionAlerta,string nombreUsuario)
        {
            var historialPeriodoIncidentes = new List<DuracionIncidente>();
            foreach (var periodoIncidente in informacionAlerta.AlertaRamal.FechasPeriodos)
            {

                var fechaInicio = TimeStampADateTime(periodoIncidente.FechaInicio);
                var fechaFin= TimeStampADateTime(periodoIncidente.FechaFin);
                var nuevoRegistro = new RegistroIncidente(0, fechaInicio, fechaFin, DateTime.Now, nombreUsuario, informacionAlerta.AlertaRamal.Causa, informacionAlerta.AlertaRamal.Efecto);
                GuardarRegistroIncidente(nuevoRegistro);
                var nuevoIncidente = new DuracionIncidente(fechaInicio, fechaFin);
                historialPeriodoIncidentes.Add(nuevoIncidente);
            }
            return historialPeriodoIncidentes;
        }
        private static DateTime TimeStampADateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        private void GuardarRegistroIncidente(RegistroIncidente nuevoRegistroIncidente)
        {
            using (var context = new RegistroIncidenteDbContext())
            {
                context.Registros.Add(nuevoRegistroIncidente);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
