using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Ssixkty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "miniatura",
                table: "media");

            migrationBuilder.DropColumn(
                name: "previsualizacion",
                table: "media");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "miniatura",
                table: "media",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "previsualizacion",
                table: "media",
                type: "text",
                nullable: true);
        }
    }
}
