using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    /// <summary>
    /// Modelo de response de ForecastController, es un modelo mas simplificado y accesible para la spa del modelo ForecastResponse
    /// </summary>
    public class ForcastResponseAbstraccion
    {
        /// <summary>
        /// Propiedad que contiene el tiempo de arribo del proximo tren
        /// </summary>
        public string TiempoArriboTren { get; set; }

        /// <summary>
        /// Propiedad que contiene el restraso que esta teniendo la linea
        /// </summary>
        public double RetrasoArrivo { get; set; }

        /// <summary>
        /// Propiedad que contiene el nombre de la estacion Origen
        /// </summary>
        public string Origen { get; set; }

        /// <summary>
        /// Propiedad que contiene el nombre de la estacion Destino
        /// </summary>
        public string Destino { get; set; }

        /// <summary>
        /// Propiedad que contiene una descripcion
        /// </summary>
        public string MensajeViaje { get; set; }

        /// <summary>
        /// Propiedad que contiene el Destino del ramal (esta propiedad coresponde al enum de tipo TipoRamalViaje)
        /// </summary>
        public int DestinoRamal { get; set; }

        /// <summary>
        /// Propiedad que cointiene el numero de estacionesRecorridas desde la estacion orgien hasta la estacion destino
        /// </summary>
        public int TotalEstacionesRecorrido { get; set; }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="destino"></param>
        public ForcastResponseAbstraccion(string origen,string destino)
        {
            this.Origen = origen;
            this.Destino = destino;
            this.MensajeViaje = $"Su viaje incia en {origen} y finaliza en {destino}";
        }
    }
}
