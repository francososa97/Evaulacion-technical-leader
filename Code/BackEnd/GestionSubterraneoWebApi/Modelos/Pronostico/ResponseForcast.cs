using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Pronostico
{
    public class ResponseForcast
    {
        public string TiempoArriboTren { get; set; }
        public double RetrasoArrivo { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string MensajeViaje { get; set; }
        public int DestinoRamal { get; set; }
        public int TotalParadasRecorrido { get; set; }

        public ResponseForcast(string origen,string destino)
        {
            this.Origen = origen;
            this.Destino = destino;
            this.MensajeViaje = $"Su viaje incia en {origen} y finaliza en {destino}";
        }
    }
}
