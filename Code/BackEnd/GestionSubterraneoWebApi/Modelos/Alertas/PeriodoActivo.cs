using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class PeriodoActivo
    {
        [JsonProperty("start")]
        public int FechaInicio { get; set; }
        [JsonProperty("end")]
        public int FechaFin { get; set; }
    }
}
