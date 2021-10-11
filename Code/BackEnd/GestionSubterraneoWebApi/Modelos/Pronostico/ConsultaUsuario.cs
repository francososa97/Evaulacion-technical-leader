using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Modelos.Pronostico
{
    public class ConsultaUsuario
    {
        public char Linea { get; set; }
        public string Estacion { get; set; }
        public RamalViaje Destino { get; set; }
        public string EstacionDestino { get; set; }

        public ConsultaUsuario(string linea, string estacion, string estacionDestino)
        {
            this.Linea = char.ToUpper(linea.ToCharArray().First());
            this.Estacion = new CultureInfo("en-US", false).TextInfo.ToTitleCase(estacion.Replace("-", " "));
            this.EstacionDestino = new CultureInfo("en-US", false).TextInfo.ToTitleCase(estacionDestino.Replace("-", " "));
        }
    }

    public enum RamalViaje
    {
        vuelta = 0,
        ida = 1,
    }
}
