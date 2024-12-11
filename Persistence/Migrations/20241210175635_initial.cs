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
                name: "categorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    oculto_por_defecto = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categorias", x => x.id);
                });

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
                name: "medias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    filename = table.Column<string>(type: "text", nullable: true),
                    hash = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    provider = table.Column<string>(type: "text", nullable: false),
                    miniatura = table.Column<string>(type: "text", nullable: true),
                    previsualizacion = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    hashed_password = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subcategorias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    nombre_corto = table.Column<string>(type: "text", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategorias", x => x.id);
                    table.ForeignKey(
                        name: "FK_subcategorias_categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "categorias",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "respuestas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contenido = table.Column<string>(type: "text", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_respuestas", x => x.id);
                    table.ForeignKey(
                        name: "FK_respuestas_encuestas_encuesta_id",
                        column: x => x.encuesta_id,
                        principalTable: "encuestas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "medias_spoileables",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    hashed_media_id = table.Column<Guid>(type: "uuid", nullable: false),
                    spoiler = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias_spoileables", x => x.id);
                    table.ForeignKey(
                        name: "FK_medias_spoileables_medias_hashed_media_id",
                        column: x => x.hashed_media_id,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "baneos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    moderador_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_baneado_id = table.Column<Guid>(type: "uuid", nullable: false),
                    concluye = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    razon = table.Column<int>(type: "integer", nullable: false),
                    mensaje = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "hilos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    autor_nombre = table.Column<string>(type: "text", nullable: false),
                    autor_rango = table.Column<string>(type: "text", nullable: false),
                    ultimo_bump = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    dados = table.Column<bool>(type: "boolean", nullable: false),
                    id_unico_activado = table.Column<bool>(type: "boolean", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    subcategoria_id = table.Column<Guid>(type: "uuid", nullable: false),
                    encuesta_id = table.Column<Guid>(type: "uuid", nullable: true),
                    portada_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
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
                        name: "FK_hilos_medias_spoileables_portada_id",
                        column: x => x.portada_id,
                        principalTable: "medias_spoileables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    autor_nombre = table.Column<string>(type: "text", nullable: false),
                    autor_rango = table.Column<string>(type: "text", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Texto = table.Column<string>(type: "text", nullable: false),
                    tag = table.Column<string>(type: "text", nullable: false),
                    media_spoileable_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tag_unico = table.Column<string>(type: "text", nullable: true),
                    dados = table.Column<int>(type: "integer", nullable: true),
                    color = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    recibir_notificaciones = table.Column<bool>(type: "boolean", nullable: false),
                    HiloId = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_hilos_HiloId",
                        column: x => x.HiloId,
                        principalTable: "hilos",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_comentarios_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentarios_medias_spoileables_media_spoileable_id",
                        column: x => x.media_spoileable_id,
                        principalTable: "medias_spoileables",
                        principalColumn: "id");
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    razon = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_denuncias_de_hilo", x => x.Id);
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
                name: "hilo_interacciones",
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
                    table.PrimaryKey("PK_hilo_interacciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_hilo_interacciones_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hilo_interacciones_usuarios_usuario_id",
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
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                name: "comentario_interacciones",
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
                    table.PrimaryKey("PK_comentario_interacciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentario_interacciones_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentario_interacciones_usuarios_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios_destacados",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentarios_destacados", x => x.id);
                    table.ForeignKey(
                        name: "FK_comentarios_destacados_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comentarios_destacados_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "denuncias_de_comentario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Razon = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    denunciante_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_denuncias_de_comentario", x => x.id);
                    table.ForeignKey(
                        name: "FK_denuncias_de_comentario_comentarios_comentario_id",
                        column: x => x.comentario_id,
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

            migrationBuilder.CreateTable(
                name: "notificaciones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    notificado_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    tipo_de_interaccion = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: false),
                    hilo_id = table.Column<Guid>(type: "uuid", nullable: true),
                    comentario_id = table.Column<Guid>(type: "uuid", nullable: true),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificaciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_notificaciones_comentarios_comentario_id",
                        column: x => x.comentario_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificaciones_comentarios_respondido_id",
                        column: x => x.respondido_id,
                        principalTable: "comentarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificaciones_hilos_hilo_id",
                        column: x => x.hilo_id,
                        principalTable: "hilos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificaciones_usuarios_notificado_id",
                        column: x => x.notificado_id,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespuestasComentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    respondido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    respuesta_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_baneos_moderador_id",
                table: "baneos",
                column: "moderador_id");

            migrationBuilder.CreateIndex(
                name: "IX_baneos_usuario_baneado_id",
                table: "baneos",
                column: "usuario_baneado_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentario_interacciones_comentario_id",
                table: "comentario_interacciones",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentario_interacciones_usuario_id",
                table: "comentario_interacciones",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_autor_id",
                table: "comentarios",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_hilo_id",
                table: "comentarios",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_HiloId",
                table: "comentarios",
                column: "HiloId");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_media_spoileable_id",
                table: "comentarios",
                column: "media_spoileable_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_destacados_comentario_id",
                table: "comentarios_destacados",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "IX_comentarios_destacados_hilo_id",
                table: "comentarios_destacados",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_comentario_comentario_id",
                table: "denuncias_de_comentario",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_comentario_denunciante_id",
                table: "denuncias_de_comentario",
                column: "denunciante_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_hilo_denunciante_id",
                table: "denuncias_de_hilo",
                column: "denunciante_id");

            migrationBuilder.CreateIndex(
                name: "IX_denuncias_de_hilo_hilo_id",
                table: "denuncias_de_hilo",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_hilo_interacciones_hilo_id",
                table: "hilo_interacciones",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_hilo_interacciones_usuario_id",
                table: "hilo_interacciones",
                column: "usuario_id");

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
                column: "portada_id");

            migrationBuilder.CreateIndex(
                name: "IX_hilos_subcategoria_id",
                table: "hilos",
                column: "subcategoria_id");

            migrationBuilder.CreateIndex(
                name: "IX_medias_hash",
                table: "medias",
                column: "hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_spoileables_hashed_media_id",
                table: "medias_spoileables",
                column: "hashed_media_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_comentario_id",
                table: "notificaciones",
                column: "comentario_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_hilo_id",
                table: "notificaciones",
                column: "hilo_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_notificado_id",
                table: "notificaciones",
                column: "notificado_id");

            migrationBuilder.CreateIndex(
                name: "IX_notificaciones_respondido_id",
                table: "notificaciones",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "IX_respuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasComentarios_respondido_id",
                table: "RespuestasComentarios",
                column: "respondido_id");

            migrationBuilder.CreateIndex(
                name: "IX_RespuestasComentarios_respuesta_id",
                table: "RespuestasComentarios",
                column: "respuesta_id");

            migrationBuilder.CreateIndex(
                name: "IX_stickies_hilo_id",
                table: "stickies",
                column: "hilo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subcategorias_CategoriaId",
                table: "subcategorias",
                column: "CategoriaId");

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
                name: "baneos");

            migrationBuilder.DropTable(
                name: "comentario_interacciones");

            migrationBuilder.DropTable(
                name: "comentarios_destacados");

            migrationBuilder.DropTable(
                name: "denuncias_de_comentario");

            migrationBuilder.DropTable(
                name: "denuncias_de_hilo");

            migrationBuilder.DropTable(
                name: "hilo_interacciones");

            migrationBuilder.DropTable(
                name: "notificaciones");

            migrationBuilder.DropTable(
                name: "respuestas");

            migrationBuilder.DropTable(
                name: "RespuestasComentarios");

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

            migrationBuilder.DropTable(
                name: "medias_spoileables");

            migrationBuilder.DropTable(
                name: "subcategorias");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "categorias");
        }
    }
}
