using System;
using System.Collections.Generic;
using System.Text;

namespace Modelos.Alertas
{
    public class DuracionIncidente
    {
        public DateTime InicioIncidente { get; set; }
        public DateTime FinIncidente { get; set; }

        public DuracionIncidente(DateTime inicioIncidente,DateTime FinIncidente)
        {
            this.InicioIncidente = inicioIncidente;
            this.FinIncidente = FinIncidente;
        }
    }
}
