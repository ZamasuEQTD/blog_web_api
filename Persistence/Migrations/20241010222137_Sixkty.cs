using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Sixkty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HiloId",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "media",
                table: "comentarios",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "respuestas_comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_respondido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_comentarios_comentarios_comentario_respondido_id",
                        column: x => x.comentario_respondido_id,
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
                name: "IX_comentarios_HiloId",
                table: "comentarios",
                column: "HiloId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_media",
                table: "comentarios",
                column: "media");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_comentarios_comentario_respondido_id",
                table: "respuestas_comentarios",
                column: "comentario_respondido_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_comentarios_respuesta_id",
                table: "respuestas_comentarios",
                column: "respuesta_id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_hilos_HiloId",
                table: "comentarios",
                column: "HiloId",
                principalTable: "hilos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_media_references_media",
                table: "comentarios",
                column: "media",
                principalTable: "media_references",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_hilos_HiloId",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_media_references_media",
                table: "comentarios");

            migrationBuilder.DropTable(
                name: "respuestas_comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_HiloId",
                table: "comentarios");

            migrationBuilder.DropIndex(
                name: "IX_comentarios_media",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "HiloId",
                table: "comentarios");

            migrationBuilder.DropColumn(
                name: "media",
                table: "comentarios");
        }
    }
}
