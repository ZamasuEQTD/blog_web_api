using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambiando_nombre_col_moderador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("20c682d2-a4d6-4a70-946f-383a6029bcb3"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("43449ed9-0816-4fe6-9f58-1da6b8b8ee27"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("c4916822-2aa9-4cd0-a594-43d024a67764"));

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "43449ed9-0816-4fe6-9f58-1da6b8b8ee27", "259a37e0-ba4d-4518-bc8d-633508c4ab7b" });

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "c4916822-2aa9-4cd0-a594-43d024a67764", "a5ab5119-296c-4b8c-b56c-dd33b824eff7" });

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("259a37e0-ba4d-4518-bc8d-633508c4ab7b"));

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("a5ab5119-296c-4b8c-b56c-dd33b824eff7"));

            migrationBuilder.RenameColumn(
                name: "Moderador",
                table: "usuarios",
                newName: "moderador");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "ConcurrencyStamp", "Name", "NormalizedName", "ShortName" },
                values: new object[,]
                {
                    { new Guid("24dd671a-da04-4a70-970e-f5ae035ea0eb"), null, "Owner", "Owner", "Owner" },
                    { new Guid("849fb1c1-fd1b-4610-9f36-fc06b1ca7cd6"), null, "Anonimo", "ANONIMO", "Anon" },
                    { new Guid("df811687-f29a-4bb8-bef6-f12617d92c4a"), null, "Moderador", "MODERADOR", "Mod" }
                });

            migrationBuilder.InsertData(
                table: "usuario_roles",
                columns: new[] { "role_id", "usuario_id" },
                values: new object[,]
                {
                    { "24dd671a-da04-4a70-970e-f5ae035ea0eb", "652cbcfa-55b6-48ff-a387-190edf444534" },
                    { "df811687-f29a-4bb8-bef6-f12617d92c4a", "ef9b81e2-fc28-44a4-81e4-5c3721b01988" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "moderador", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "registrado_en", "SecurityStamp", "TwoFactorEnabled", "username" },
                values: new object[,]
                {
                    { new Guid("652cbcfa-55b6-48ff-a387-190edf444534"), 0, "f54951ea-ee6b-4e5d-97cb-befe0a972d18", null, false, false, null, "Zamasu", null, null, "$2a$13$E7xC4yIP3yDjj3Z.0.RWRe1HUmoYJuOmljkdc0zGVnnVhXldZucDC", null, false, new DateTime(2024, 12, 25, 14, 0, 27, 985, DateTimeKind.Utc).AddTicks(3331), null, false, "Owner1223" },
                    { new Guid("ef9b81e2-fc28-44a4-81e4-5c3721b01988"), 0, "ca655dd5-c55c-4fc0-81a3-564045d15d28", null, false, false, null, "Zamasus", null, null, "$2a$13$fiGv2tm3Zp3Wp3F8dUs3I.kCGYyQ9FpkPB0v8JfM82r4rtwpL/3ta", null, false, new DateTime(2024, 12, 25, 14, 0, 27, 464, DateTimeKind.Utc).AddTicks(2796), null, false, "Moderador" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("24dd671a-da04-4a70-970e-f5ae035ea0eb"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("849fb1c1-fd1b-4610-9f36-fc06b1ca7cd6"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("df811687-f29a-4bb8-bef6-f12617d92c4a"));

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "24dd671a-da04-4a70-970e-f5ae035ea0eb", "652cbcfa-55b6-48ff-a387-190edf444534" });

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "df811687-f29a-4bb8-bef6-f12617d92c4a", "ef9b81e2-fc28-44a4-81e4-5c3721b01988" });

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("652cbcfa-55b6-48ff-a387-190edf444534"));

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("ef9b81e2-fc28-44a4-81e4-5c3721b01988"));

            migrationBuilder.RenameColumn(
                name: "moderador",
                table: "usuarios",
                newName: "Moderador");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "ConcurrencyStamp", "Name", "NormalizedName", "ShortName" },
                values: new object[,]
                {
                    { new Guid("20c682d2-a4d6-4a70-946f-383a6029bcb3"), null, "Anonimo", "ANONIMO", "Anon" },
                    { new Guid("43449ed9-0816-4fe6-9f58-1da6b8b8ee27"), null, "Moderador", "MODERADOR", "Mod" },
                    { new Guid("c4916822-2aa9-4cd0-a594-43d024a67764"), null, "Owner", "Owner", "Owner" }
                });

            migrationBuilder.InsertData(
                table: "usuario_roles",
                columns: new[] { "role_id", "usuario_id" },
                values: new object[,]
                {
                    { "43449ed9-0816-4fe6-9f58-1da6b8b8ee27", "259a37e0-ba4d-4518-bc8d-633508c4ab7b" },
                    { "c4916822-2aa9-4cd0-a594-43d024a67764", "a5ab5119-296c-4b8c-b56c-dd33b824eff7" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Moderador", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "registrado_en", "SecurityStamp", "TwoFactorEnabled", "username" },
                values: new object[,]
                {
                    { new Guid("259a37e0-ba4d-4518-bc8d-633508c4ab7b"), 0, "a880c577-c148-43e5-9cb8-d9e9e8037de2", null, false, false, null, "Zamasus", null, null, "$2a$13$ME9OsVKDp3yN3KEBPxmGMuPfcowVZkDPSxoA01bLSoYRUDntO2p6W", null, false, new DateTime(2024, 12, 23, 23, 49, 51, 307, DateTimeKind.Utc).AddTicks(3864), null, false, "Moderador" },
                    { new Guid("a5ab5119-296c-4b8c-b56c-dd33b824eff7"), 0, "fe18fbf6-5d83-40aa-8c4e-75853aae18d5", null, false, false, null, "Zamasu", null, null, "$2a$13$H23FNTtHwso7WrUdkdimjOphe2Fzp9iC7LXPOxcvWiBZKUq02xtnC", null, false, new DateTime(2024, 12, 23, 23, 49, 51, 831, DateTimeKind.Utc).AddTicks(5109), null, false, "Owner1223" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_respuestas_encuestas_encuesta_id",
                table: "respuestas",
                column: "encuesta_id",
                principalTable: "encuestas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
