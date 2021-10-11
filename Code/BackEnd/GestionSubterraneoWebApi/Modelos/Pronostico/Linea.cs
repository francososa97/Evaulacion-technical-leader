using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class Linea
    {
        [JsonProperty("Trip_Id")]
        public string IdViaje { get; set; }
        [JsonProperty("Route_Id")]
        public string IdRuta { get; set; }
        [JsonProperty("Direction_ID")]
        public int IdDireccion { get; set; }
        [JsonProperty("start_time")]
        public string HoraInicio { get; set; }
        [JsonProperty("start_date")]
        public string FechaInicio { get; set; }
        [JsonProperty("Estaciones")]
        public Estacion[] EstacionesLinea { get; set; }


    }
}
