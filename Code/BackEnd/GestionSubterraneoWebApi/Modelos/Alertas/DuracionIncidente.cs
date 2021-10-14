using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    /// <summary>
    /// Modelo que cointiene la duracion de un incidente de subterraneo
    /// </summary>
    public class DuracionIncidente
    {
        /// <summary>
        /// Tiempo de inicio de un incidente
        /// </summary>
        public DateTime InicioIncidente { get; set; }
        /// <summary>
        /// Tiempo de fin de un incidente
        /// </summary>
        public DateTime FinIncidente { get; set; }

        /// <summary>
        /// Constructor por parametros de fecha de inicio y fecha de fin
        /// </summary>
        /// <param name="inicioIncidente"></param>
        /// <param name="FinIncidente"></param>
        public DuracionIncidente(DateTime inicioIncidente,DateTime FinIncidente)
        {
            this.InicioIncidente = inicioIncidente;
            this.FinIncidente = FinIncidente;
        }
    }
}
