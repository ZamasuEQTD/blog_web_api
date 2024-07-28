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
            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "medias_references",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "media_providers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Path = table.Column<string>(type: "text", nullable: true),
                    Url = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_providers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "medias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Provider = table.Column<Guid>(type: "uuid", nullable: false),
                    discriminatro = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Thumbnail = table.Column<Guid>(type: "uuid", nullable: true),
                    Previsualizacion = table.Column<Guid>(type: "uuid", nullable: true),
                    Video_Thumbnail = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias", x => x.id);
                    table.ForeignKey(
                        name: "FK_medias_media_providers_Provider",
                        column: x => x.Provider,
                        principalTable: "media_providers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medias_medias_Previsualizacion",
                        column: x => x.Previsualizacion,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medias_medias_Thumbnail",
                        column: x => x.Thumbnail,
                        principalTable: "medias",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_medias_medias_Video_Thumbnail",
                        column: x => x.Video_Thumbnail,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medias_sources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Media = table.Column<Guid>(type: "uuid", nullable: false),
                    discriminator = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medias_sources", x => x.id);
                    table.ForeignKey(
                        name: "FK_medias_sources_medias_Media",
                        column: x => x.Media,
                        principalTable: "medias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_medias_references_MediaId",
                table: "medias_references",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_medias_Previsualizacion",
                table: "medias",
                column: "Previsualizacion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_Provider",
                table: "medias",
                column: "Provider",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_Thumbnail",
                table: "medias",
                column: "Thumbnail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_Video_Thumbnail",
                table: "medias",
                column: "Video_Thumbnail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_medias_sources_Media",
                table: "medias_sources",
                column: "Media",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_medias_references_medias_sources_MediaId",
                table: "medias_references",
                column: "MediaId",
                principalTable: "medias_sources",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medias_references_medias_sources_MediaId",
                table: "medias_references");

            migrationBuilder.DropTable(
                name: "medias_sources");

            migrationBuilder.DropTable(
                name: "medias");

            migrationBuilder.DropTable(
                name: "media_providers");

            migrationBuilder.DropIndex(
                name: "IX_medias_references_MediaId",
                table: "medias_references");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "medias_references");
        }
    }
}
