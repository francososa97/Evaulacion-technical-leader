using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{

    /// <summary>
    /// Modelo de respuesta de la api /subtes/forecastGTFS
    /// </summary>
    public class ForecastResponse
    {
        /// <summary>
        /// Propiedad que encapsula un listado de todos los ramales del subterraneo
        /// </summary>
        [JsonProperty("Entity")]
        public Ramal[] Entidad { get; set; }
    }

    /// <summary>
    /// Modelo que encapsula la informacion sobre un ramal del subterrano
    /// </summary>
    public class Ramal
    {

        /// <summary>
        /// Propiedad que identifica el id de un ramal
        /// </summary>
        [JsonProperty("ID")]
        public string Id { get; set; }

        /// <summary>
        /// Propiedad que contiene la informacion sobre el ramal del subterrano 
        /// </summary>
        [JsonProperty("Linea")]
        public Linea LineaRamal { get; set; }
    }

    /// <summary>
    /// Modelo que contiene la informacion sobre una linea del subterraneo (siendo la linea A,B,C,D,E o H)
    /// </summary>
    public class Linea
    {
        /// <summary>
        /// Propiedad que cotiene el id del viaje de la Linea
        /// </summary>
        [JsonProperty("Trip_Id")]
        public string IdViaje { get; set; }

        /// <summary>
        /// Propiedad que cotiene el id del viaje de la Ruta
        /// </summary>
        [JsonProperty("Route_Id")]
        public string IdRuta { get; set; }

        /// <summary>
        /// Propiedad que cotiene el id del viaje de la Ruta
        /// </summary>
        [JsonProperty("Direction_ID")]
        public int IdDireccion { get; set; }

        /// <summary>
        /// Propiedad que contiene la hora de inicio de la linea
        /// </summary>
        [JsonProperty("start_time")]
        public string HoraInicio { get; set; }

        /// <summary>
        /// Propiedad que contiene la fecha de inicio de la linea
        /// </summary>
        [JsonProperty("start_date")]
        public string FechaInicio { get; set; }

        /// <summary>
        /// Propiedad que contiene un listado las estaciones de una linea selccionada
        /// </summary>
        [JsonProperty("Estaciones")]
        public Estacion[] EstacionesLinea { get; set; }

    }

    /// <summary>
    /// Modelo que contiene la informacion de una estacion de subterrano
    /// </summary>
    public class Estacion
    {
        /// <summary>
        /// Propiedad que contiene el id de la estacion de una linea seleccionada
        /// </summary>
        [JsonProperty("stop_id")]
        public string EstacionId { get; set; }

        /// <summary>
        /// Propiedad que contiene el nombre de la estacion
        /// </summary>
        [JsonProperty("stop_name")]
        public string EstacionNombre { get; set; }

        /// <summary>
        /// Propiedad que contiene la informacion correspondiente a la llegada de la estacion del subterraneo
        /// </summary>
        [JsonProperty("arrival")]
        public InformacionLLegadaOSalida Llegada { get; set; }

        /// <summary>
        /// Propiedad que contiene la informacion correspondiente a la Salida de la estacion del subterraneo
        /// </summary>
        [JsonProperty("departure")]
        public InformacionLLegadaOSalida Salida { get; set; }
    }

    /// <summary>
    /// Modelo que contiene la informacion del momento de llegada y salida del subterrano
    /// </summary>
    public class InformacionLLegadaOSalida
    {
        /// <summary>
        /// Propiedad que contine el momento de llegada o salida representado en un unix timespan
        /// </summary>
        [JsonProperty("time")]
        public int Tiempo { get; set; }

        /// <summary>
        /// Propiedad que contine el retraso de llegada representado en un unix timespan
        /// </summary>
        [JsonProperty("delay")]
        public int Retraso { get; set; }
    }

}
