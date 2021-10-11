using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class ForecastResponse
    {
        [JsonProperty("Entity")]
        public Ramal[] Entidad { get; set; }
    }
}
