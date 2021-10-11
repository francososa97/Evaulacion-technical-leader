using GestionSubterraneoWebApi.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionSubterraneoWebApi.Data
{
    public class RegistroIncidenteDbContext : DbContext
    {
        public RegistroIncidenteDbContext(DbContextOptions<RegistroIncidenteDbContext> options) : base(options) { }

        public RegistroIncidenteDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-S20AD2M\\SQLEXPRESS;Database=Incidientes;User ID=gestionIncidente;Password=ErGp9H8zzT4YYcrvQEpy; TrustServerCertificate=True;MultipleActiveResultSets=True")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(MyLoggerFactory);
        }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
               .AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information)
               .AddConsole();
        });

        public DbSet<RegistroIncidente> Registros { get; set; }

    }
}
