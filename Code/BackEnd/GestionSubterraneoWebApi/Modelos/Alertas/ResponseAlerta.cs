using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class ResponseAlerta
    {
        [JsonProperty("Entity")]
        public DetalleAlerta AlertaRamal { get; set; }
    }
}
