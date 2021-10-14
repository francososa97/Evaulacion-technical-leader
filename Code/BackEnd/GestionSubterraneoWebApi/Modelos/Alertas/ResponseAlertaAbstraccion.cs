using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    /// <summary>
    /// Modelo de response de AlertasController, es un modelo mas simplificado y accesible para la spa del modelo ResponseAlerta
    /// </summary>
    public class ResponseAlertaAbstraccion
    {
        /// <summary>
        /// Propiedad que contiene la causa de demora de una alerta
        /// </summary>
        public string CausaDemora { get; set; }
        /// <summary>
        /// Propiedad que contiene la Efecto de demora de una alerta
        /// </summary>
        public string EfectoIncidentes { get; set; }
        /// <summary>
        /// Propiedad que contiene la descripcion de la alerta
        /// </summary>
        public string DescripcionIncidentes { get; set; }
        /// <summary>
        /// Propiedad que contiene el ramal afecatdo por el incidente
        /// </summary>
        public string RamalAfectado { get; set; }
        /// <summary>
        /// Propiedad que cointiene la duracion de un incidente de subterraneo
        /// </summary>
        public List<DuracionIncidente> PeriodoIncidentes { get; set; }
        /// <summary>
        /// Propiedad que contiene el tipo de efecto del incidente en el subterraneo
        /// </summary>
        public TipoEfecto TipoEfecto { get; set; }
        /// <summary>
        /// Propiedad que contiene el tipo de causa del incidente en el subterraneo
        /// </summary>
        public TipoCausa TipoCausa { get; set; }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="causa"></param>
        /// <param name="efecto"></param>
        /// <param name="descripcionDemara"></param>
        /// <param name="ramalAfectado"></param>
        public ResponseAlertaAbstraccion(TipoCausa causa,TipoEfecto efecto,string descripcionDemara,string ramalAfectado)
        {
            this.TipoCausa = causa;
            this.TipoEfecto = efecto;
            this.CausaDemora = causa.ToString();
            this.EfectoIncidentes = efecto.ToString();
            this.DescripcionIncidentes = descripcionDemara;
            this.RamalAfectado = ramalAfectado;
            this.PeriodoIncidentes = new List<DuracionIncidente>();
        }
        /// <summary>
        /// Constructor por default
        /// </summary>
        public ResponseAlertaAbstraccion()
        {
            this.TipoEfecto = TipoEfecto.NO_SERVICE;
            this.TipoCausa = TipoCausa.UNKNOWN_CAUSE;
            this.PeriodoIncidentes = new List<DuracionIncidente>();
        }
    }

    /// <summary>
    /// Enumerable que contiene el tipo de efecto por un incidente
    /// </summary>
    public enum TipoEfecto
    {
        NO_SERVICE = 0,
        REDUCED_SERVICE = 1,
        SIGNIFICANT_DELAYS = 2,
        DETOUR = 3,
        ADDITIONAL_SERVICE = 4,
        MODIFIED_SERVICE = 5,
        OTHER_EFFECT = 6,
        UNKNOWN_EFFECT = 7,
        STOP_MOVED = 8,
    }

    /// <summary>
    /// Enumerable que contiene el tipo de causa por un incidente
    /// </summary>
    public enum TipoCausa
    {
        UNKNOWN_CAUSE = 0,
        OTHER_CAUSE = 1,
        TECHNICAL_PROBLEM = 2,
        STRIKE = 3,
        DEMONSTRATION = 4,
        ACCIDENT = 5,
        HOLIDAY = 6,
        WEATHER = 7,
        MAINTENANCE = 8,
        CONSTRUCTION = 9,
        POLICE_ACTIVITY = 10,
        MEDICAL_EMERGENCY = 11,
    }
}
