using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class agregado : Migration
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
                keyValue: new Guid("9fc3e2ff-0383-4e6a-8c83-de558b87cb50"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("d7cb2025-d0cf-4b35-b515-5f43b00ca0fa"));

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("f430e9a8-491a-4bd7-b76a-4d0f4eefcfb1"));

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "d7cb2025-d0cf-4b35-b515-5f43b00ca0fa", "13b20cc1-9fea-4008-b532-f1327e1a847f" });

            migrationBuilder.DeleteData(
                table: "usuario_roles",
                keyColumns: new[] { "role_id", "usuario_id" },
                keyValues: new object[] { "f430e9a8-491a-4bd7-b76a-4d0f4eefcfb1", "fe654252-460a-494c-b41f-a7789b51b00b" });

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("13b20cc1-9fea-4008-b532-f1327e1a847f"));

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: new Guid("fe654252-460a-494c-b41f-a7789b51b00b"));

            migrationBuilder.DropColumn(
                name: "status",
                table: "notificaciones");

            migrationBuilder.AddColumn<bool>(
                name: "leida",
                table: "notificaciones",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.DropColumn(
                name: "leida",
                table: "notificaciones");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "notificaciones",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "ConcurrencyStamp", "Name", "NormalizedName", "ShortName" },
                values: new object[,]
                {
                    { new Guid("9fc3e2ff-0383-4e6a-8c83-de558b87cb50"), null, "Anonimo", "ANONIMO", "Anon" },
                    { new Guid("d7cb2025-d0cf-4b35-b515-5f43b00ca0fa"), null, "Owner", "Owner", "Owner" },
                    { new Guid("f430e9a8-491a-4bd7-b76a-4d0f4eefcfb1"), null, "Moderador", "MODERADOR", "Mod" }
                });

            migrationBuilder.InsertData(
                table: "usuario_roles",
                columns: new[] { "role_id", "usuario_id" },
                values: new object[,]
                {
                    { "d7cb2025-d0cf-4b35-b515-5f43b00ca0fa", "13b20cc1-9fea-4008-b532-f1327e1a847f" },
                    { "f430e9a8-491a-4bd7-b76a-4d0f4eefcfb1", "fe654252-460a-494c-b41f-a7789b51b00b" }
                });

            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Moderador", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "registrado_en", "SecurityStamp", "TwoFactorEnabled", "username" },
                values: new object[,]
                {
                    { new Guid("13b20cc1-9fea-4008-b532-f1327e1a847f"), 0, "97ff121b-d3ce-45bf-87a2-632021cb9cb2", null, false, false, null, "Zamasu", null, null, "$2a$13$es2ZlzY4OuMsPk.M5d6JpOOQOl5WfIqRM8GtfkcGq6aEe2osVTH7S", null, false, new DateTime(2024, 12, 23, 23, 37, 26, 226, DateTimeKind.Utc).AddTicks(6869), null, false, "Owner1223" },
                    { new Guid("fe654252-460a-494c-b41f-a7789b51b00b"), 0, "6241f465-bae9-42d8-aa03-abb03158657d", null, false, false, null, "Zamasus", null, null, "$2a$13$L08zPDFIY5PHizF9FHC2EO0XWFZ3J9T/dtxrxr.uc08eStNPWX0Jm", null, false, new DateTime(2024, 12, 23, 23, 37, 25, 695, DateTimeKind.Utc).AddTicks(6233), null, false, "Moderador" }
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
