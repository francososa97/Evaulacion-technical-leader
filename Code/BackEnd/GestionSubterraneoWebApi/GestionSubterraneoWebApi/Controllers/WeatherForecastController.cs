using GestionSubterraneoWebApi.Data;
using GestionSubterraneoWebApi.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RegistroIncidenteDbContext _context;

        public WeatherForecastController(RegistroIncidenteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return "Test";
        }
    }
}
