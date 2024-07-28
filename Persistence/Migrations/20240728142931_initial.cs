using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "encuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encuestas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medias_references",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    spoiler = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias_references", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    Rango = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "respuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Contenido = table.Column<string>(type: "text", nullable: false),
                    EncuestaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "encuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hilos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    decripcion = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    portada_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dados_activados = table.Column<bool>(type: "boolean", nullable: false),
                    id_unico_activado = table.Column<bool>(type: "boolean", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hilos", x => x.id);
                    table.ForeignKey(
                        name: "FK_hilos_encuestas_encuesta_id",
                        column: x => x.encuesta_id,
                        principalTable: "encuestas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_hilos_medias_references_portada_id",
                        column: x => x.portada_id,
                        principalTable: "medias_references",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hilos_usuarios_autor_id",
                        column: x => x.autor_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    VotanteId = table.Column<Guid>(type: "uuid", nullable: false),
                    EncuestaId = table.Column<Guid>(type: "uuid", nullable: false),
                    RespuestaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votos", x => x.id);
                    table.ForeignKey(
                        name: "FK_votos_encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "encuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votos_respuestas_RespuestaId",
                        column: x => x.RespuestaId,
                        principalTable: "respuestas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votos_usuarios_VotanteId",
                        column: x => x.VotanteId,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    HiloId = table.Column<Guid>(type: "uuid", nullable: false),
                    tag = table.Column<string>(type: "text", nullable: false),
                    dados = table.Column<int>(type: "integer", nullable: true),
                    tag_unico = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    texto = table.Column<string>(type: "text", nullable: false),
                    Destacado = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_hilos_HiloId",
                        column: x => x.HiloId,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_AutorId",
                        column: x => x.AutorId,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_AutorId",
                table: "comentarios",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_HiloId",
                table: "comentarios",
                column: "HiloId");

            migrationBuilder.CreateIndex(
                name: "IX_hilos_autor_id",
                table: "hilos",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_hilos_encuesta_id",
                table: "hilos",
                column: "encuesta_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_hilos_portada_id",
                table: "hilos",
                column: "portada_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_EncuestaId",
                table: "respuestas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_votos_EncuestaId",
                table: "votos",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_votos_RespuestaId",
                table: "votos",
                column: "RespuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_votos_VotanteId",
                table: "votos",
                column: "VotanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "votos");

            migrationBuilder.DropTable(
                name: "hilos");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "medias_references");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "encuestas");
        }
    }
}
