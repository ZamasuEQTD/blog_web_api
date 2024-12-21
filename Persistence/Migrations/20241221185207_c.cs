using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class c : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_Hilos_HiloId1",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_Hilos_hilo_id",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_destacados_Hilos_hilo_id",
                table: "comentarios_destacados");

            migrationBuilder.DropForeignKey(
                name: "FK_denuncias_de_hilo_Hilos_hilo_id",
                table: "denuncias_de_hilo");

            migrationBuilder.DropForeignKey(
                name: "FK_hilo_interacciones_Hilos_hilo_id",
                table: "hilo_interacciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Hilos_encuestas_encuesta_id",
                table: "Hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hilos_medias_spoileables_portada_id",
                table: "Hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hilos_subcategorias_subcategoria_id",
                table: "Hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_Hilos_usuarios_autor_id",
                table: "Hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_notificaciones_Hilos_hilo_id",
                table: "notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_stickies_Hilos_hilo_id",
                table: "stickies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hilos",
                table: "Hilos");

            migrationBuilder.RenameTable(
                name: "Hilos",
                newName: "[Hilos]");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_subcategoria_id",
                table: "[Hilos]",
                newName: "IX_[Hilos]_subcategoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_portada_id",
                table: "[Hilos]",
                newName: "IX_[Hilos]_portada_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_encuesta_id",
                table: "[Hilos]",
                newName: "IX_[Hilos]_encuesta_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_autor_id",
                table: "[Hilos]",
                newName: "IX_[Hilos]_autor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_[Hilos]",
                table: "[Hilos]",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_[Hilos]_encuestas_encuesta_id",
                table: "[Hilos]",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_[Hilos]_medias_spoileables_portada_id",
                table: "[Hilos]",
                column: "portada_id",
                principalTable: "medias_spoileables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_[Hilos]_subcategorias_subcategoria_id",
                table: "[Hilos]",
                column: "subcategoria_id",
                principalTable: "subcategorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_[Hilos]_usuarios_autor_id",
                table: "[Hilos]",
                column: "autor_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_[Hilos]_HiloId1",
                table: "comentarios",
                column: "HiloId1",
                principalTable: "[Hilos]",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_[Hilos]_hilo_id",
                table: "comentarios",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_destacados_[Hilos]_hilo_id",
                table: "comentarios_destacados",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_denuncias_de_hilo_[Hilos]_hilo_id",
                table: "denuncias_de_hilo",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilo_interacciones_[Hilos]_hilo_id",
                table: "hilo_interacciones",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notificaciones_[Hilos]_hilo_id",
                table: "notificaciones",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_stickies_[Hilos]_hilo_id",
                table: "stickies",
                column: "hilo_id",
                principalTable: "[Hilos]",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_[Hilos]_encuestas_encuesta_id",
                table: "[Hilos]");

            migrationBuilder.DropForeignKey(
                name: "FK_[Hilos]_medias_spoileables_portada_id",
                table: "[Hilos]");

            migrationBuilder.DropForeignKey(
                name: "FK_[Hilos]_subcategorias_subcategoria_id",
                table: "[Hilos]");

            migrationBuilder.DropForeignKey(
                name: "FK_[Hilos]_usuarios_autor_id",
                table: "[Hilos]");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_[Hilos]_HiloId1",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_[Hilos]_hilo_id",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_destacados_[Hilos]_hilo_id",
                table: "comentarios_destacados");

            migrationBuilder.DropForeignKey(
                name: "FK_denuncias_de_hilo_[Hilos]_hilo_id",
                table: "denuncias_de_hilo");

            migrationBuilder.DropForeignKey(
                name: "FK_hilo_interacciones_[Hilos]_hilo_id",
                table: "hilo_interacciones");

            migrationBuilder.DropForeignKey(
                name: "FK_notificaciones_[Hilos]_hilo_id",
                table: "notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_stickies_[Hilos]_hilo_id",
                table: "stickies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_[Hilos]",
                table: "[Hilos]");

            migrationBuilder.RenameTable(
                name: "[Hilos]",
                newName: "Hilos");

            migrationBuilder.RenameIndex(
                name: "IX_[Hilos]_subcategoria_id",
                table: "Hilos",
                newName: "IX_Hilos_subcategoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_[Hilos]_portada_id",
                table: "Hilos",
                newName: "IX_Hilos_portada_id");

            migrationBuilder.RenameIndex(
                name: "IX_[Hilos]_encuesta_id",
                table: "Hilos",
                newName: "IX_Hilos_encuesta_id");

            migrationBuilder.RenameIndex(
                name: "IX_[Hilos]_autor_id",
                table: "Hilos",
                newName: "IX_Hilos_autor_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hilos",
                table: "Hilos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_Hilos_HiloId1",
                table: "comentarios",
                column: "HiloId1",
                principalTable: "Hilos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_Hilos_hilo_id",
                table: "comentarios",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_destacados_Hilos_hilo_id",
                table: "comentarios_destacados",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_denuncias_de_hilo_Hilos_hilo_id",
                table: "denuncias_de_hilo",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilo_interacciones_Hilos_hilo_id",
                table: "hilo_interacciones",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hilos_encuestas_encuesta_id",
                table: "Hilos",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hilos_medias_spoileables_portada_id",
                table: "Hilos",
                column: "portada_id",
                principalTable: "medias_spoileables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hilos_subcategorias_subcategoria_id",
                table: "Hilos",
                column: "subcategoria_id",
                principalTable: "subcategorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hilos_usuarios_autor_id",
                table: "Hilos",
                column: "autor_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notificaciones_Hilos_hilo_id",
                table: "notificaciones",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_stickies_Hilos_hilo_id",
                table: "stickies",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
