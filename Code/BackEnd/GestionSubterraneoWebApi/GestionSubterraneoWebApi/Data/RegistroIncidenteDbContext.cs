using GestionSubterraneoWebApi.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modelos.Base_de_datos;

namespace GestionSubterraneoWebApi.Data
{
    public class RegistroIncidenteDbContext : DbContext
    {
        /// <summary>
        /// Constructor con parametros de la clase padre DbContext
        /// </summary>
        /// <param name="options"></param>
        public RegistroIncidenteDbContext(DbContextOptions<RegistroIncidenteDbContext> options) : base(options) { }

        /// <summary>
        /// Constructor sin parametros de la clase DbContext
        /// </summary>
        public RegistroIncidenteDbContext() : base() { }

        /// <summary>
        /// Metodo de configuracion de enrtity framework
        /// </summary>
        /// <param name="options"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-S20AD2M\\SQLEXPRESS;Database=Incidientes;User ID=gestionIncidente;Password=ErGp9H8zzT4YYcrvQEpy; TrustServerCertificate=True;MultipleActiveResultSets=True")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(MyLoggerFactory);
        }

        /// <summary>
        /// Metodo por el cual obtenemos un log mas especifico
        /// </summary>
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
               .AddFilter((category, level) =>
                   category == DbLoggerCategory.Database.Command.Name
                   && level == LogLevel.Information)
               .AddConsole();
        });

        /// <summary>
        /// DbSet de la tabla Causa en sql server
        /// </summary>
        public DbSet<Causa> CausaIncidente { get; set; }
        /// <summary>
        /// DbSet de la tabla EfectoIncidente en sql server
        /// </summary>
        public DbSet<Efecto> EfectoIncidente { get; set; }
        /// <summary>
        /// DbSet de la tabla Registros en sql server
        /// </summary>
        public DbSet<RegistroIncidente> Registros { get; set; }

    }
}
