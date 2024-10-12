using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fifity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_EncuestaId",
                table: "respuestas");

            migrationBuilder.DropIndex(
                name: "IX_respuestas_EncuestaId",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "EncuestaId",
                table: "respuestas");

            migrationBuilder.AddColumn<Guid>(
                name: "encuesta_id",
                table: "respuestas",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id");

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

            migrationBuilder.DropIndex(
                name: "IX_respuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropColumn(
                name: "encuesta_id",
                table: "respuestas");

            migrationBuilder.AddColumn<Guid>(
                name: "EncuestaId",
                table: "respuestas",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_EncuestaId",
                table: "respuestas",
                column: "EncuestaId");

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_EncuestaId",
                table: "respuestas",
                column: "EncuestaId",
                principalTable: "encuestas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
