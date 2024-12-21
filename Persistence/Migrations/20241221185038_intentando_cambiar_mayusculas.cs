using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class intentando_cambiar_mayusculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_hilos_HiloId",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_hilos_hilo_id",
                table: "comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_comentarios_destacados_hilos_hilo_id",
                table: "comentarios_destacados");

            migrationBuilder.DropForeignKey(
                name: "FK_denuncias_de_hilo_hilos_hilo_id",
                table: "denuncias_de_hilo");

            migrationBuilder.DropForeignKey(
                name: "FK_hilo_interacciones_hilos_hilo_id",
                table: "hilo_interacciones");

            migrationBuilder.DropForeignKey(
                name: "FK_hilos_encuestas_encuesta_id",
                table: "hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_hilos_medias_spoileables_portada_id",
                table: "hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_hilos_subcategorias_subcategoria_id",
                table: "hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_hilos_usuarios_autor_id",
                table: "hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_notificaciones_hilos_hilo_id",
                table: "notificaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_stickies_hilos_hilo_id",
                table: "stickies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hilos",
                table: "hilos");

            migrationBuilder.RenameTable(
                name: "hilos",
                newName: "Hilos");

            migrationBuilder.RenameIndex(
                name: "IX_hilos_subcategoria_id",
                table: "Hilos",
                newName: "IX_Hilos_subcategoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_hilos_portada_id",
                table: "Hilos",
                newName: "IX_Hilos_portada_id");

            migrationBuilder.RenameIndex(
                name: "IX_hilos_encuesta_id",
                table: "Hilos",
                newName: "IX_Hilos_encuesta_id");

            migrationBuilder.RenameIndex(
                name: "IX_hilos_autor_id",
                table: "Hilos",
                newName: "IX_Hilos_autor_id");

            migrationBuilder.RenameColumn(
                name: "HiloId",
                table: "comentarios",
                newName: "HiloId1");

            migrationBuilder.RenameIndex(
                name: "IX_comentarios_HiloId",
                table: "comentarios",
                newName: "IX_comentarios_HiloId1");

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
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_stickies_Hilos_hilo_id",
                table: "stickies",
                column: "hilo_id",
                principalTable: "Hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "hilos");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_subcategoria_id",
                table: "hilos",
                newName: "IX_hilos_subcategoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_portada_id",
                table: "hilos",
                newName: "IX_hilos_portada_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_encuesta_id",
                table: "hilos",
                newName: "IX_hilos_encuesta_id");

            migrationBuilder.RenameIndex(
                name: "IX_Hilos_autor_id",
                table: "hilos",
                newName: "IX_hilos_autor_id");

            migrationBuilder.RenameColumn(
                name: "HiloId1",
                table: "comentarios",
                newName: "HiloId");

            migrationBuilder.RenameIndex(
                name: "IX_comentarios_HiloId1",
                table: "comentarios",
                newName: "IX_comentarios_HiloId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hilos",
                table: "hilos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_hilos_HiloId",
                table: "comentarios",
                column: "HiloId",
                principalTable: "hilos",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_hilos_hilo_id",
                table: "comentarios",
                column: "hilo_id",
                principalTable: "hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comentarios_destacados_hilos_hilo_id",
                table: "comentarios_destacados",
                column: "hilo_id",
                principalTable: "hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_denuncias_de_hilo_hilos_hilo_id",
                table: "denuncias_de_hilo",
                column: "hilo_id",
                principalTable: "hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilo_interacciones_hilos_hilo_id",
                table: "hilo_interacciones",
                column: "hilo_id",
                principalTable: "hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_encuestas_encuesta_id",
                table: "hilos",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_medias_spoileables_portada_id",
                table: "hilos",
                column: "portada_id",
                principalTable: "medias_spoileables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_subcategorias_subcategoria_id",
                table: "hilos",
                column: "subcategoria_id",
                principalTable: "subcategorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_usuarios_autor_id",
                table: "hilos",
                column: "autor_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notificaciones_hilos_hilo_id",
                table: "notificaciones",
                column: "hilo_id",
                principalTable: "hilos",
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
                name: "FK_stickies_hilos_hilo_id",
                table: "stickies",
                column: "hilo_id",
                principalTable: "hilos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
