using Modelos.Alertas;
using Modelos.Base_de_datos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Modelos
{
    /// <summary>
    /// Modelo que encapsula la informacion de la tabla Registro de SQL server por entity framework
    /// </summary>
    public class RegistroIncidente
    {
        /// <summary>
        /// Propiedad que contiene el incide de la tabla Registro de la base de datos
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Propiedad que contiene la fecha de inicio de un Incidente
        /// </summary>
        [Required]
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Propiedad que contiene la fecha de fin de un Incidente
        /// </summary>
        [Required]
        public DateTime FechaFin { get; set; }

        /// <summary>
        /// Propiedad que contiene la fecha cuando se realiza la consulta de incidentes
        /// </summary>
        [Required]
        public DateTime FechaConsultado { get; set; }
        /// <summary>
        /// Propiedad que contiene el nombre de usuario que consulta los incidentes
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Objeto que encapsula el modelo de causa de incidente
        /// </summary>
        [Required]
        [ForeignKey("FK_Registros_CausaIncidente_IdCausa")]
        public Causa Causa { get; set; }

        /// <summary>
        /// Objeto que encapsula el modelo de causa de incidente
        /// </summary>
        [Required]
        [ForeignKey("FK_Registros_EfectoIncidente_IdEfecto")]
        public Efecto Efecto { get; set; }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="fechaConsultado"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="causa"></param>
        /// <param name="efecto"></param>
        public RegistroIncidente(int id,DateTime fechaInicio, DateTime fechaFin, DateTime fechaConsultado, string nombreUsuario,TipoCausa causa,TipoEfecto efecto)
        {
            Id = id;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FechaConsultado = fechaConsultado;
            NombreUsuario = nombreUsuario;
            Causa = new Causa((int)causa);
            Efecto = new Efecto((int)efecto);
        }

        /// <summary>
        /// Constructor por default
        /// </summary>
        public RegistroIncidente()
        {
            Causa = new Causa();
            Efecto = new Efecto();
        }


    }
}
