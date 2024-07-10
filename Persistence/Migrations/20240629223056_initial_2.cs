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
            migrationBuilder.DropForeignKey(
                name: "FK_hilos_medias_PortadaId",
                table: "hilos");

            migrationBuilder.DropColumn(
                name: "spoiler",
                table: "medias");

            migrationBuilder.AddColumn<string>(
                name: "hash",
                table: "medias",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "medias_references",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    spoiler = table.Column<bool>(type: "boolean", nullable: false),
                    MediaId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias_references", x => x.id);
                    table.ForeignKey(
                        name: "FK_medias_references_medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medias_references_MediaId",
                table: "medias_references",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_medias_references_PortadaId",
                table: "hilos",
                column: "PortadaId",
                principalTable: "medias_references",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hilos_medias_references_PortadaId",
                table: "hilos");

            migrationBuilder.DropTable(
                name: "medias_references");

            migrationBuilder.DropColumn(
                name: "hash",
                table: "medias");

            migrationBuilder.AddColumn<bool>(
                name: "spoiler",
                table: "medias",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_hilos_medias_PortadaId",
                table: "hilos",
                column: "PortadaId",
                principalTable: "medias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
