using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    /// <summary>
    /// Modelo de respuesta de la api /subtes/serviceAlerts
    /// </summary>
    public class ResponseAlerta
    {
        /// <summary>
        /// Modelo que encapsula informacion detallada sobre incidentes y efectos sobre las lineas de subterraneo
        /// </summary>
        [JsonProperty("Entity")]
        public DetalleAlerta[] AlertaRamal { get; set; }
    }
    /// <summary>
    /// Modelo que encapsula informacion detallada sobre incidentes y efectos sobre las lineas de subterraneo
    /// </summary>
    public class DetalleAlerta
    {
        /// <summary>
        /// Propiedad que almacena el del ramal contiene la linea del ramal ej: LineaA
        /// </summary>
        [JsonProperty("id")]
        public string AlertaId { get; set; }

        /// <summary>
        /// Propiedad que almacena un flag para dar aviso si el incidente es activo o no
        /// </summary>
        [JsonProperty("is_deleted")]
        public bool EsActivo { get; set; }

        /// <summary>
        /// Propiedad que almacena la actualizacion del viaje, por lo general viene como null
        /// </summary>
        [JsonProperty("trip_update")]
        public string ActualizacionViaje { get; set; }

        /// <summary>
        /// Propiedad que almacena el Veiculo, por lo general viene como null
        /// </summary>
        [JsonProperty("vehicle")]
        public string Veiculo { get; set; }

        /// <summary>
        /// Propiedad que almacena la informacion detallada de la alerta
        /// </summary>
        [JsonProperty("alert")]
        public Alerta AlertaIncidente{ get; set; }

    }

    /// <summary>
    /// Modelo que almacena la informacion detallada de la alerta
    /// </summary>
    public class Alerta
    {
        /// <summary>
        /// propiedad que encapsula todos los periodos de Incidentes
        /// </summary>
        [JsonProperty("active_period")]
        public PeriodoActivo[] FechasPeriodos { get; set; }

        /// <summary>
        /// propiedad que encapsula informacion detallada sobre el ramal y su respectivo incidente
        /// </summary>
        [JsonProperty("informed_entity")]
        public InformacionAdicionalEntidad[] InformacionRamal { get; set; }

        /// <summary>
        /// propiedad que encapsula la causa original del incidente, lo guardamos como int pero despues es mapeado a un enum TipoCausa
        /// </summary>
        [JsonProperty("cause")]
        public int Causa { get; set; }
        /// <summary>
        /// propiedad que encapsula la efecto original del incidente, lo guardamos como int pero despues es mapeado a un enum TipoEfecto
        /// </summary>
        [JsonProperty("effect")]
        public int Efecto { get; set; }

        /// <summary>
        /// Propiedad que encapusla una url con mas detalle del incidente
        /// </summary>
        [JsonProperty("url")]
        public string UrlIncidente { get; set; }

        /// <summary>
        /// propiedad que encapsula la descripcion del incidente
        /// </summary>
        [JsonProperty("header_text")]
        public DescripcionDemora InformacionAlerta { get; set; }
        /// <summary>
        /// propiedad que encapsula la descripcion del incidente
        /// </summary>
        [JsonProperty("description_text")]
        public DescripcionDemora MotivosDemoras { get; set; }
    }


    /// <summary>
    /// Modelo que encapsula todos los periodos de Incidentes
    /// </summary>
    public class PeriodoActivo
    {
        /// <summary>
        /// Propiedad que encapsula la fecha de inicio del incidente en formato unix timespan
        /// </summary>
        [JsonProperty("start")]
        public int FechaInicio { get; set; }
        /// <summary>
        /// Propiedad que encapsula la fecha de fin del incidente en formato unix timespan
        /// </summary>
        [JsonProperty("end")]
        public int FechaFin { get; set; }
    }

    /// <summary>
    /// Modelo que encapsula informacion detallada sobre el ramal y su respectivo incidente - este modelo no se utiliza en la spa ya que es un modelo que se comparte con trenes/serviceAlerts y con /colectivos/serviceAlerts
    /// </summary>
    public class InformacionAdicionalEntidad
    {
        /// <summary>
        /// propiedad que encapsula el string idAgencia
        /// </summary>
        [JsonProperty("agency_id")]
        public string IdAgencia { get; set; }
        /// <summary>
        /// propiedad que encapsula el string IdRuta
        /// </summary>
        [JsonProperty("route_id")]
        public string IdRuta { get; set; }
        /// <summary>
        /// propiedad que encapsula el int TypoRuta
        /// </summary>
        [JsonProperty("route_type")]
        public int? TypoRuta { get; set; }
        /// <summary>
        /// propiedad que encapsula el int ParadaId
        /// </summary>
        [JsonProperty("stop_id")]
        public int? ParadaId { get; set; }
    }
    /// <summary>
    /// propiedad que encapsula la descripcion del incidente
    /// </summary>
    public class DescripcionDemora
    {
        /// <summary>
        /// Propiedad que encapsula un listado con demoras en diferentes traducciones
        /// </summary>
        [JsonProperty("translation")]
        public Traducion[] TraduccionDemora { get; set; }

    }

    /// <summary>
    /// Modelo que encapsula un listado con demoras en diferentes traducciones
    /// </summary>
    public class Traducion
    {
        /// <summary>
        /// propiedad que encapsula la descripcion del incidente
        /// </summary>
        [JsonProperty("text")]
        public string Descripcion { get; set; }
        /// <summary>
        /// propiedad que encapsula el lenguaje de la descripcion
        /// </summary>
        [JsonProperty("language")]
        public string Lenguaje { get; set; }
    }
}
