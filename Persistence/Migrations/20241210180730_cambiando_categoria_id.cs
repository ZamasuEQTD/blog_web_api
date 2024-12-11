using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambiando_categoria_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_subcategorias_categorias_CategoriaId",
                table: "subcategorias");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "subcategorias",
                newName: "categoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_subcategorias_CategoriaId",
                table: "subcategorias",
                newName: "IX_subcategorias_categoria_id");

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_subcategorias_categorias_categoria_id",
                table: "subcategorias",
                column: "categoria_id",
                principalTable: "categorias",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DropForeignKey(
                name: "FK_subcategorias_categorias_categoria_id",
                table: "subcategorias");

            migrationBuilder.RenameColumn(
                name: "categoria_id",
                table: "subcategorias",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_subcategorias_categoria_id",
                table: "subcategorias",
                newName: "IX_subcategorias_CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_subcategorias_categorias_CategoriaId",
                table: "subcategorias",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "id");
        }
    }
}
