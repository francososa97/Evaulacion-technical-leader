using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class Ramal
    {
        [JsonProperty("ID")]
        public string Id { get; set; }
        [JsonProperty("Linea")]
        public Linea LineaRamal { get; set; }
    }
}
