using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "denuncias_de_comentario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentarios_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Razon = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_denuncias_de_comentario", x => x.id);
                    table.ForeignKey(
                        name: "FK_denuncias_de_comentario_comentarios_comentarios_id",
                        column: x => x.comentarios_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_denuncias_de_comentario_usuarios_denunciante_id",
                        column: x => x.denunciante_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_comentario_comentarios_id",
                table: "denuncias_de_comentario",
                column: "comentarios_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_comentario_denunciante_id",
                table: "denuncias_de_comentario",
                column: "denunciante_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "denuncias_de_comentario");
        }
    }
}
