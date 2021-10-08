using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionSubterraneoWebApi.Migrations
{
    public partial class Registrosinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaConsultado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCausa = table.Column<int>(type: "int", nullable: false),
                    IdEfecto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");
        }
    }
}
