using GestionSubterraneoWebApi.Modelos;
using Modelos.Alertas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Modelos.Base_de_datos
{
    /// <summary>
    /// Modelo que encapsula la informacion de la tabla Causa de SQL server por entity framework
    /// </summary>
    public class Causa
    {

        /// <summary>
        /// Propiedad que contiene el incide de la tabla Causa de la base de datos
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Propiedad que contiene la descripcion de la causa de un incidente
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
        public Causa(int id)
        {
            this.RegistrosId = id;
            this.Descripcion = ((TipoEfecto)id).ToString();
        }
        /// <summary>
        /// Constructor por default
        /// </summary>
        public Causa() { }


    }
}
