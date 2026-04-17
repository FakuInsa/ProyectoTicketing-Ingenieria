using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoTicketing_Ingenieria.Migrations
{
    /// <inheritdoc />
    public partial class ModeloInicialTicketing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auditorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    Accion = table.Column<string>(type: "TEXT", nullable: false),
                    RecursoAfectado = table.Column<string>(type: "TEXT", nullable: false),
                    RecursoId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "TEXT", precision: 3, nullable: false),
                    Detalle = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Lugar = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GoogleSubjectId = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacidad = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectores_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Butacas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Fila = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroAsiento = table.Column<int>(type: "INTEGER", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaBloqueo = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Butacas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Butacas_Sectores_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ButacaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Expiracion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Butacas_ButacaId",
                        column: x => x.ButacaId,
                        principalTable: "Butacas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Butacas_SectorId",
                table: "Butacas",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ButacaId",
                table: "Reservas",
                column: "ButacaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectores_EventoId",
                table: "Sectores",
                column: "EventoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditorias");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Butacas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Sectores");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
