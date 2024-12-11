using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambiando_col_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropTable(
                name: "RespuestasComentarios");

            migrationBuilder.RenameColumn(
                name: "Texto",
                table: "comentarios",
                newName: "texto");

            migrationBuilder.CreateTable(
                name: "respuestas_comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_comentarios_comentarios_respondido_id",
                        column: x => x.respondido_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_respuestas_comentarios_comentarios_respuesta_id",
                        column: x => x.respuesta_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_comentarios_respondido_id",
                table: "respuestas_comentarios",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_comentarios_respuesta_id",
                table: "respuestas_comentarios",
                column: "respuesta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropTable(
                name: "respuestas_comentarios");

            migrationBuilder.RenameColumn(
                name: "texto",
                table: "comentarios",
                newName: "Texto");

            migrationBuilder.CreateTable(
                name: "RespuestasComentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespuestasComentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_RespuestasComentarios_comentarios_respondido_id",
                        column: x => x.respondido_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespuestasComentarios_comentarios_respuesta_id",
                        column: x => x.respuesta_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasComentarios_respondido_id",
                table: "RespuestasComentarios",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasComentarios_respuesta_id",
                table: "RespuestasComentarios",
                column: "respuesta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
