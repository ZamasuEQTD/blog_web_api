using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "baneos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moderador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_baneado_id = table.Column<Guid>(type: "uuid", nullable: false),
                    concluye = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    mensaje = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baneos", x => x.id);
                    table.ForeignKey(
                        name: "FK_baneos_usuarios_moderador_id",
                        column: x => x.moderador_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_baneos_usuarios_usuario_baneado_id",
                        column: x => x.usuario_baneado_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_baneos_moderador_id",
                table: "baneos",
                column: "moderador_id");

            migrationBuilder.CreateIndex(
                name: "IX_baneos_usuario_baneado_id",
                table: "baneos",
                column: "usuario_baneado_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baneos");
        }
    }
}
