using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class DescripcionDemora
    {
        [JsonProperty("text")]
        public string Descripcion { get; set; }
        [JsonProperty("language")]
        public string Lenguaje { get; set; }
    }
}
