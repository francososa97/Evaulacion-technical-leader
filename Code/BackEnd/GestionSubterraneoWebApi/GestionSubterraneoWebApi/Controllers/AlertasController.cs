using GestionSubterraneoWebApi.Data;
using GestionSubterraneoWebApi.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlertasController : ControllerBase
    {
        private readonly RegistroIncidenteDbContext _context;

        public AlertasController(RegistroIncidenteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroIncidente>>> Get()
        {
            return await _context.Registros.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<RegistroIncidente> GetById(int id)
        {
            var Registros = _context.Registros.ToList();
            var RegistroSeleccionado = Registros.Where(Registro => Registro.Id == id).ToList().FirstOrDefault();

            if(RegistroSeleccionado == null)
            {
                var mensajeError = NotFound("El registro de incidente con id:" + id.ToString() + " no existe.");
                return mensajeError;
            }

            return Ok(Response);
        }
    }
}
