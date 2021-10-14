using Microsoft.AspNetCore.Mvc;
using Modelos.Pronostico;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Controllers
{
    /// <summary>
    /// este controlador Gestiona las llamadas y peticiones con respecto a la api /subtes/forecastGTFS
    /// </summary>
    [ApiController]
    [Route("forecast")]
    public class ForecastController : ControllerBase
    {
        #region Atributos
        /// <summary>
        /// Atributo privado en el que se almacena el modelo de respuesta de /subtes/forecastGTFS
        /// </summary>
        private ForecastResponse InformacionRamales { get; set; }
        #endregion

        #region Metodos endpoints

        /// <summary>
        /// Metodo que encapsuula la llamada a /subtes/forecastGTFS y por el cual se obtiene el proximo subterraneo a llegar a la estacion seleccionada
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="linea"></param>
        /// <param name="estacion"></param>
        /// <param name="destino"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("usuario={nombreUsuario}&linea={linea}&estacion={estacion}&destino={destino}")]
        public ForcastResponseAbstraccion GetForecast(string nombreUsuario, string linea, string estacion, string destino)
        {
            var nombreCifrado = GestionSubterraneoWebApi.Helper.GestionSubterraneoHelper.ObtenerClienteId(nombreUsuario);
            var consultaActiva = new ConsultaUsuario(linea, estacion, destino);
            this.InformacionRamales = GetForecastGTFS(nombreCifrado).Result;
            var proximaLinea = BuscarProximoSubterraneo(consultaActiva, InformacionRamales);
            return proximaLinea;
        }

        /// <summary>
        /// Metodo por el cual se obtiene un array de strings(strings[]) con todas las estaciones de una linea seleccionada
        /// </summary>
        /// <param name="linea"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("estacion/linea={linea}&usuario={nombreUsuario}")]
        public IActionResult GetEstacion(string linea, string nombreUsuario)
        {
            var nombreCifrado = GestionSubterraneoWebApi.Helper.GestionSubterraneoHelper.ObtenerClienteId(nombreUsuario);
            this.InformacionRamales = GetForecastGTFS(nombreCifrado).Result;
            var estaciones = ObtenerEstacionesRamal(this.InformacionRamales, linea);
            return Ok(estaciones);
        }

        #endregion

        #region Metodos Privados

        /// <summary>
        ///  Metodo privado que encapsula la informacion de la llamada a la api /subtes/forecastGTFS y retorna un modelo abstraido de la llamada
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        private async Task<ForecastResponse> GetForecastGTFS(string clientId)
        {
            ForecastResponse ForecastCliente = new ForecastResponse();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string clientSecret = "1";
                    var endPoint = $"https://apitransporte.buenosaires.gob.ar/subtes/forecastGTFS?client_id={clientId}&client_secret={clientSecret}";
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

        /// <summary>
        /// Metodo privado por el cual se obitene el proximo subterraneo a llegar a la estacion origen
        /// </summary>
        /// <param name="ElementoBuscar"></param>
        /// <param name="informacionLineas"></param>
        /// <returns></returns>
        private ForcastResponseAbstraccion BuscarProximoSubterraneo(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas)
        {
            ForcastResponseAbstraccion response = new ForcastResponseAbstraccion(ElementoBuscar.Estacion, ElementoBuscar.EstacionDestino);
            ElementoBuscar.Destino = ObtenerRamalViaje(ElementoBuscar, informacionLineas);
            string idRamalaBuscar = $"Linea{ElementoBuscar.Linea}_{ElementoBuscar.Linea}{(int)ElementoBuscar.Destino}";
            ObtenerArriboProgramado(ElementoBuscar, informacionLineas, idRamalaBuscar, response);
            response.DestinoRamal = (int)ElementoBuscar.Destino;
            response.TotalEstacionesRecorrido = ObtenerTotalEstaciones(ElementoBuscar, informacionLineas, idRamalaBuscar);
            return response;
        }

        /// <summary>
        /// Metodo privado que encapsula la infromacion de la orientacion del ramal subterraneo( si el viaje es de ida o de vuelta ej: peru a plaza de mayo o plaza de mayo a peru )
        /// </summary>
        /// <param name="ElementoBuscar"></param>
        /// <param name="informacionLineas"></param>
        /// <returns></returns>
        private TipoRamalViaje ObtenerRamalViaje(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas)
        {
            string idRamalaBuscar = $"Linea{ElementoBuscar.Linea}_{ElementoBuscar.Linea}1";
            var estaciones = new List<string>();
            double arriboProgramado;
            TipoRamalViaje orientacionViaje;

            foreach (var ramal in informacionLineas.Entidad)
            {
                if (ramal.Id.Contains(idRamalaBuscar))
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        //si primero esta la estacion y despues es el destino es ese ramal si no esl otro ramal
                        if (ElementoBuscar.EstacionDestino == estacion.EstacionNombre)
                        {
                            estaciones.Add(estacion.EstacionNombre);
                            arriboProgramado = new TimeSpan(Convert.ToInt64(estacion.Llegada.Tiempo) + Convert.ToInt64(estacion.Llegada.Retraso)).TotalMinutes;
                        }
                        if (ElementoBuscar.Estacion == estacion.EstacionNombre)
                        {
                            estaciones.Add(estacion.EstacionNombre);
                        }
                    }
                }
            }
            //Comentar esto y pasarlo a un metodo
            if (estaciones.First() == ElementoBuscar.Estacion)
                orientacionViaje = TipoRamalViaje.ida;
            else
                orientacionViaje = TipoRamalViaje.vuelta;

            return orientacionViaje;
        }

        /// <summary>
        /// Metodo privado que obtiene el tiempo estimado de llegada del subterraneo
        /// </summary>
        /// <param name="ElementoBuscar"></param>
        /// <param name="informacionLineas"></param>
        /// <param name="idRamalaBuscar"></param>
        /// <param name="response"></param>
        private void ObtenerArriboProgramado(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas, string idRamalaBuscar, ForcastResponseAbstraccion response)
        {

            foreach (var ramal in informacionLineas.Entidad)
            {
                if (ramal.Id.Contains(idRamalaBuscar))
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        if (ElementoBuscar.Estacion == estacion.EstacionNombre)
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

        /// <summary>
        ///  Metodo por el cual se obtiene la cantidad total de estaciones desde la estacion incial hasta la estacion final
        /// </summary>
        /// <param name="ElementoBuscar"></param>
        /// <param name="informacionLineas"></param>
        /// <param name="idRamalaBuscar"></param>
        /// <returns></returns>
        private int ObtenerTotalEstaciones(ConsultaUsuario ElementoBuscar, ForecastResponse informacionLineas, string idRamalaBuscar)
        {
            int cantidadParadasEnRecorrido = 0;
            bool paradaEnRecorrido = false;
            foreach (var ramal in informacionLineas.Entidad)
            {
                if (ramal.Id.Contains(idRamalaBuscar))
                {
                    foreach (var estacion in ramal.LineaRamal.EstacionesLinea)
                    {
                        if (ElementoBuscar.Estacion == estacion.EstacionNombre || paradaEnRecorrido)
                        {
                            paradaEnRecorrido = true;
                            cantidadParadasEnRecorrido++;
                            if (ElementoBuscar.EstacionDestino == estacion.EstacionNombre)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return cantidadParadasEnRecorrido;
        }

        /// <summary>
        /// Metodo por el cual se obtienen un listado de todas las estaciones de una lina seleccionada
        /// </summary>
        /// <param name="InformacionRamales"></param>
        /// <param name="linea"></param>
        /// <returns></returns>
        private List<string> ObtenerEstacionesRamal(ForecastResponse InformacionRamales, string linea)
        {
            var estaciones = new List<string>();
            string lineaId = $"Linea{linea.ToUpper()}";
            foreach (var entidad in InformacionRamales.Entidad)
            {
                if (entidad.LineaRamal.IdRuta == lineaId && entidad.LineaRamal.IdDireccion == 1)
                {
                    foreach (var estacion in entidad.LineaRamal.EstacionesLinea)
                    {
                        estaciones.Add(estacion.EstacionNombre);
                    }
                }

            }

            return estaciones;
        }
        #endregion
    }
}
