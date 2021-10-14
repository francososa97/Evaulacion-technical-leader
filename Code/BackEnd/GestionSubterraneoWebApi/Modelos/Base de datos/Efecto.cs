using GestionSubterraneoWebApi.Modelos;
using Modelos.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos.Base_de_datos
{
    /// <summary>
    /// Modelo que encapsula la informacion de la tabla Efecto de SQL server por entity framework
    /// </summary>
    public class Efecto
    {
        /// <summary>
        /// Propiedad que contiene el incide de la tabla Efecto de la base de datos
        /// </summary>
        [Required]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Propiedad que contiene la descripcion de la Efecto de un incidente
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Propiedad que contiene el id del registro (FK)
        /// </summary>
        public int RegistrosId { get; set; }

        /// <summary>
        /// Constructor por parametros
        /// </summary>
        /// <param name="id"></param>
        public Efecto(int id)
        {
            this.RegistrosId = id;
            this.Descripcion = ((TipoCausa)id).ToString();
        }

        /// <summary>
        /// Constructor por default
        /// </summary>
        public Efecto(){}
    }
}
