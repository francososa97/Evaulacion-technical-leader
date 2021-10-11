using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class DetalleAlerta
    {
        [JsonProperty("active_period")]
        public PeriodoActivo[] FechasPeriodos { get; set; }
        [JsonProperty("informed_entity")]
        public InformacionAdicionalEntidad[] InformacionRamal { get; set; }
        [JsonProperty("cause")]
        public int Causa { get; set; }
        [JsonProperty("effect")]
        public int Efecto { get; set; }
        [JsonProperty("header_text")]
        public DescripcionDemora[] InformacionAlerta {get;set;}
        [JsonProperty("description_text")]
        public DescripcionDemora[] MotivosDemoras { get; set; }
    }
}
