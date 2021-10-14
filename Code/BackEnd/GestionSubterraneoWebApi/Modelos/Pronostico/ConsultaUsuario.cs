using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Modelos.Pronostico
{
    /// <summary>
    /// Modelo que encapsulo la consulta del usuario a registrar
    /// </summary>
    public class ConsultaUsuario
    {
        /// <summary>
        /// Propiedad que encapsula la linea seleccionada por el usuario
        /// </summary>
        public char Linea { get; set; }

        /// <summary>
        /// Propiedad que encapsula la Estacion seleccionada por el usuario
        /// </summary>
        public string Estacion { get; set; }

        /// <summary>
        /// Propiedad que encapsula el enum de tipo Ramal viaje
        /// </summary>
        public TipoRamalViaje Destino { get; set; }

        /// <summary>
        /// Propiedad que encapsula la estacion destino seleccionada por el usuario
        /// </summary>
        public string EstacionDestino { get; set; }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="estacion"></param>
        /// <param name="estacionDestino"></param>
        public ConsultaUsuario(string linea, string estacion, string estacionDestino)
        {
            this.Linea = char.ToUpper(linea.ToCharArray().First());
            this.Estacion = new CultureInfo("en-US", false).TextInfo.ToTitleCase(estacion.Replace("-", " "));
            this.EstacionDestino = new CultureInfo("en-US", false).TextInfo.ToTitleCase(estacionDestino.Replace("-", " "));
        }
    }
    /// <summary>
    /// enum - Tipo ramal que informa la direccion si es de ida (peru a plaza de mayo) o vuelta (plaza de mayo a peru)
    /// </summary>
    public enum TipoRamalViaje
    {
        vuelta = 0,
        ida = 1,
    }
}
