using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medias_medias_Video_Thumbnail",
                table: "medias");

            migrationBuilder.DropIndex(
                name: "IX_medias_Video_Thumbnail",
                table: "medias");

            migrationBuilder.DropColumn(
                name: "Video_Thumbnail",
                table: "medias");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Video_Thumbnail",
                table: "medias",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_Video_Thumbnail",
                table: "medias",
                column: "Video_Thumbnail",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_medias_medias_Video_Thumbnail",
                table: "medias",
                column: "Video_Thumbnail",
                principalTable: "medias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
