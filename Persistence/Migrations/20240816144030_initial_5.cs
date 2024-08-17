using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "encuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_encuestas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hilos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subcategoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    dados = table.Column<bool>(type: "boolean", nullable: false),
                    id_unico_activado = table.Column<bool>(type: "boolean", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                        name: "FK_hilos_subcategorias_subcategoria_id",
                        column: x => x.subcategoria_id,
                        principalTable: "subcategorias",
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
                name: "respuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contenido = table.Column<string>(type: "text", nullable: false),
                    EncuestaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "votos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    votante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    EncuestaId = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_votos_usuarios_votante_id",
                        column: x => x.votante_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    destacado = table.Column<bool>(type: "boolean", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    encuesta_id = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentarios_usuarios_autor_id",
                        column: x => x.autor_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "denuncias_de_hilo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Razon = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_denuncias_de_hilo", x => x.id);
                    table.ForeignKey(
                        name: "FK_denuncias_de_hilo_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_denuncias_de_hilo_usuarios_denunciante_id",
                        column: x => x.denunciante_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "relaciones_de_hilo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    seguido = table.Column<bool>(type: "boolean", nullable: false),
                    favorito = table.Column<bool>(type: "boolean", nullable: false),
                    oculto = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relaciones_de_hilo", x => x.id);
                    table.ForeignKey(
                        name: "FK_relaciones_de_hilo_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relaciones_de_hilo_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stickies",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    concluye_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stickies", x => x.id);
                    table.ForeignKey(
                        name: "FK_stickies_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "relaciones_de_comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    oculto = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relaciones_de_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_relaciones_de_comentarios_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_relaciones_de_comentarios_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_autor_id",
                table: "comentarios",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_hilo_id",
                table: "comentarios",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_hilo_denunciante_id",
                table: "denuncias_de_hilo",
                column: "denunciante_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_hilo_hilo_id",
                table: "denuncias_de_hilo",
                column: "hilo_id");

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
                name: "IX_hilos_subcategoria_id",
                table: "hilos",
                column: "subcategoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_relaciones_de_comentarios_comentario_id",
                table: "relaciones_de_comentarios",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "IX_relaciones_de_comentarios_usuario_id",
                table: "relaciones_de_comentarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_relaciones_de_hilo_hilo_id",
                table: "relaciones_de_hilo",
                column: "hilo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_relaciones_de_hilo_usuario_id",
                table: "relaciones_de_hilo",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_EncuestaId",
                table: "respuestas",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_stickies_hilo_id",
                table: "stickies",
                column: "hilo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_votos_EncuestaId",
                table: "votos",
                column: "EncuestaId");

            migrationBuilder.CreateIndex(
                name: "IX_votos_votante_id",
                table: "votos",
                column: "votante_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "denuncias_de_hilo");

            migrationBuilder.DropTable(
                name: "relaciones_de_comentarios");

            migrationBuilder.DropTable(
                name: "relaciones_de_hilo");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "stickies");

            migrationBuilder.DropTable(
                name: "votos");

            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "hilos");

            migrationBuilder.DropTable(
                name: "encuestas");
        }
    }
}
