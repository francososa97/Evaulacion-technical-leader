using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class InformacionLLegadaOSalida
    {
        [JsonProperty("time")]
        public int Tiempo { get; set; }
        [JsonProperty("delay")]
        public int Retraso { get; set; }
    }
}
