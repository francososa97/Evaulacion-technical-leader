using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class Estacion
    {
        [JsonProperty("stop_id")]
        public string ParadaId { get; set; }
        [JsonProperty("stop_name")]
        public string ParadaNombre { get; set; }
        [JsonProperty("arrival")]
        public InformacionLLegadaOSalida Llegada { get; set; }
        [JsonProperty("departure")]
        public InformacionLLegadaOSalida Salida { get; set; }
    }
}
