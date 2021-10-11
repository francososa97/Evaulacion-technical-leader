using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class InformacionAdicionalEntidad
    {
        [JsonProperty("agency_id")]
        public string IdAgencia { get; set; }
        [JsonProperty("route_id")]
        public string IdRuta { get; set; }
        [JsonProperty("route_type")]
        public int? TypoRuta { get; set; }
        [JsonProperty("stop_id")]
        public int? ParadaId { get; set; }
    }
}
