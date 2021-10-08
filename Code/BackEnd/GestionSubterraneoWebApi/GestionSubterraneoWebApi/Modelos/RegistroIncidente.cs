using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Modelos
{
    public class RegistroIncidente
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FechaInicio { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
        [Required]
        public DateTime FechaConsultado { get; set; }
        public string NombreUsuario { get; set; }
        [Required]
        public int IdCausa { get; set; }
        [Required]
        public int IdEfecto { get; set; }

        public RegistroIncidente(int id,DateTime fechaInicio, DateTime fechaFin, DateTime fechaConsultado, string nombreUsuario, int idCausa, int idEfecto)
        {
            Id = id;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaConsultado = fechaConsultado;
            NombreUsuario = nombreUsuario;
            IdCausa = idCausa;
            IdEfecto = idEfecto;
        }
    }
}
