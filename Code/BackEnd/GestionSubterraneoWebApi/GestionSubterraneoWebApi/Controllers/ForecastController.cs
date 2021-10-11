using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Pronostico;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Controllers
{
    [ApiController]
    [Route("forecast")]
    public class ForecastController : ControllerBase
    {
        private ForecastResponse InformacionRamales { get; set; }

        [HttpGet]
        [Route("linea={linea}&estacion={estacion}&destino={destino}")]
        public ResponseForcast Get(string linea,string estacion,string destino)
        {
            var consultaActiva = new ConsultaUsuario(linea, estacion, destino);
            var response = GetForecastGTFS();
            this.InformacionRamales = response.Result;
            var proximaLinea = BuscarProximoTren(consultaActiva, response.Result);
            return proximaLinea;
        }

        [HttpGet]
        [Route("estacion/linea={linea}")]
        public IActionResult GetEstacion(string linea)
        {
            if(InformacionRamales == null)
            {
                this.InformacionRamales = GetForecastGTFS().Result;
            }
            var estaciones = ObtenerEstacionesRamal(this.InformacionRamales, linea);
            return Ok(estaciones);
        }


        #region Metodos Privados
        private async Task<ForecastResponse> GetForecastGTFS()
        {
            ForecastResponse ForecastCliente = new ForecastResponse();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string idClient = "1";
                    string clientSecret = "1";
                    var endPoint =$"https://apitransporte.buenosaires.gob.ar/subtes/forecastGTFS?client_id={idClient}&client_secret={clientSecret}";
                    client.BaseAddress = new Uri(endPoint);
                    client.Timeout = TimeSpan.FromSeconds(20);
                    HttpResponseMessage response = await client.GetAsync(endPoint);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseJson = await response.Content.ReadAsStringAsync();
                        ForecastCliente = JsonConvert.DeserializeObject<ForecastResponse>(responseJson);
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

        private ResponseForcast BuscarProximoTren(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas)
        {
            ResponseForcast response = new ResponseForcast(ElementoBuscar.Estacion, ElementoBuscar.EstacionDestino);
            ElementoBuscar.Destino = ObtenerRamalViaje(ElementoBuscar,informacionLineas);
            string idRamalaBuscar = $"Linea{ElementoBuscar.Linea}_{ElementoBuscar.Linea}{(int)ElementoBuscar.Destino}2";
            ObtenerArriboProgramado(ElementoBuscar, informacionLineas, idRamalaBuscar, response);
            response.DestinoRamal = (int)ElementoBuscar.Destino;
            response.TotalParadasRecorrido = ObtenerTotalParadas(ElementoBuscar, informacionLineas, idRamalaBuscar);
            return response;
        }
        private RamalViaje ObtenerRamalViaje(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas)
        {
            string idRamalaBuscar = $"Linea{ElementoBuscar.Linea}_{ElementoBuscar.Linea}1";
            var estaciones = new List<string>();
            double arriboProgramado;
            RamalViaje orientacionViaje;

            foreach (var ramal in informacionLineas.Entidad)
            {
                if (ramal.Id.Contains(idRamalaBuscar))
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        //si primero esta la estacion y despues es el destino es ese ramal si no esl otro ramal
                        if (ElementoBuscar.EstacionDestino == estacion.ParadaNombre)
                        {
                            estaciones.Add(estacion.ParadaNombre);
                            arriboProgramado = new TimeSpan(Convert.ToInt64(estacion.Llegada.Tiempo) + Convert.ToInt64(estacion.Llegada.Retraso)).TotalMinutes;
                        }
                        if (ElementoBuscar.Estacion == estacion.ParadaNombre)
                        {
                            estaciones.Add(estacion.ParadaNombre);
                        }
                    }
                }
            }
            //Comentar esto y pasarlo a un metodo
            if (estaciones.First() == ElementoBuscar.Estacion)
                orientacionViaje = RamalViaje.ida;
            else
                orientacionViaje = RamalViaje.vuelta;

            return orientacionViaje;
        }
        private void ObtenerArriboProgramado(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas,string idRamalaBuscar, ResponseForcast response)
        {

            foreach (var ramal in informacionLineas.Entidad)
            {
                if (ramal.Id.Contains(idRamalaBuscar))
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        if (ElementoBuscar.Estacion == estacion.ParadaNombre)
                        {
                            var Totaltimellegada = new TimeSpan(Convert.ToInt64(estacion.Llegada.Tiempo + estacion.Llegada.Retraso));
                            response.TiempoArriboTren = Totaltimellegada.ToString(@"mm\:ss");
                            response.RetrasoArrivo = new TimeSpan(Convert.ToInt64(estacion.Llegada.Retraso)).TotalMinutes;
                            return;
                        }
                    }
                    
                }
            }
            return;
        }

        private int ObtenerTotalParadas(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas, string idRamalaBuscar)
        {
            int cantidadParadasEnRecorrido = 0;
            bool paradaEnRecorrido = false;
             foreach (var ramal in informacionLineas.Entidad)
            {
                if(ramal.Id == idRamalaBuscar)
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        if (ElementoBuscar.Estacion == estacion.ParadaNombre || paradaEnRecorrido)
                        {
                            paradaEnRecorrido = true;
                            cantidadParadasEnRecorrido++;
                            if (ElementoBuscar.EstacionDestino == estacion.ParadaNombre)
                            {
                                break;
                            }
                        }
                    }
                }
            }
                return cantidadParadasEnRecorrido;
        }

        private List<string> ObtenerEstacionesRamal(ForecastResponse InformacionRamales,string linea)
        {
            var estaciones = new List<string>();
            string lineaId = $"Linea{linea.ToUpper()}";
            foreach (var entidad in InformacionRamales.Entidad)
            {
                if(entidad.LineaRamal.IdRuta == lineaId && entidad.LineaRamal.IdDireccion == 1)
                {
                    foreach(var estacion in entidad.LineaRamal.EstacionesLinea)
                    {
                        estaciones.Add(estacion.ParadaNombre);
                    }
                }

            }

            return estaciones;
        }
        #endregion



    }
}
