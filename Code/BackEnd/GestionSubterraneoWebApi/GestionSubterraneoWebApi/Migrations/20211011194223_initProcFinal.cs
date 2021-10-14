using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GestionSubterraneoWebApi.Migrations
{
    /// <summary>
    /// Ultima migracion a la base de datos => initProcFinal
    /// </summary>
    public partial class initProcFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CausaIncidente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CausaIncidente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EfectoIncidente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfectoIncidente", x => x.Id);
                });

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
                    FK_Registros_CausaIncidente_IdCausa = table.Column<int>(type: "int", nullable: false),
                    FK_Registros_EfectoIncidente_IdEfecto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registros_CausaIncidente_FK_Registros_CausaIncidente_IdCausa",
                        column: x => x.FK_Registros_CausaIncidente_IdCausa,
                        principalTable: "CausaIncidente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registros_EfectoIncidente_FK_Registros_EfectoIncidente_IdEfecto",
                        column: x => x.FK_Registros_EfectoIncidente_IdEfecto,
                        principalTable: "EfectoIncidente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registros_FK_Registros_CausaIncidente_IdCausa",
                table: "Registros",
                column: "FK_Registros_CausaIncidente_IdCausa");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_FK_Registros_EfectoIncidente_IdEfecto",
                table: "Registros",
                column: "FK_Registros_EfectoIncidente_IdEfecto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "CausaIncidente");

            migrationBuilder.DropTable(
                name: "EfectoIncidente");
        }
    }
}
