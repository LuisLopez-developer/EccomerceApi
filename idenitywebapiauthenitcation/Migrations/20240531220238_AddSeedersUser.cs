using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedersUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "admin", "ADMIN" },
                    { "2", null, "user", "USER" },
                    { "3", null, "managed", "MANAGED" }
                });

            migrationBuilder.InsertData(
                table: "Peoples",
                columns: new[] { "Id", "Address", "LastName", "Name" },
                values: new object[,]
                {
                    { 1, "123 Elm St", "Lopez", "Luis" },
                    { 2, "431 Elm St", "Muñoz", "Jeampierre" },
                    { 3, "233 Elm St", "benedicto", "Jean" },
                    { 4, "432 Elm St", "Zambrano", "Fabrizzio" },
                    { 5, "32 Elm St", "Ambrosio", "Fabian" }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" },
                    { 3, "En espera" },
                    { 4, "En proceso" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PeopleId", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StateId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "d65ec53a-2020-413c-8994-c65d87c6148c", "AppUser", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEMuMkP5/t6MHfGE8v1Zj5staqUcheQHcNHqlrg6qs600c9PG51X3FkYRL61NIqd9gw==", 1, null, false, "", 1, false, "admin" },
                    { "2", 0, "35c56708-a162-47f6-a9bb-a9849d77c6d4", "AppUser", "user@example.com", true, false, null, "USER@EXAMPLE.COM", "USER", "AQAAAAIAAYagAAAAED9dohuh5NaTLZECxAtQL9wWeDTcWuGxTpOl+Kt+ChzfH7nJCbfw9Sp4baFLb3RDCg==", 2, null, false, "", 1, false, "user" },
                    { "3", 0, "882b9c8f-d920-4a00-b45b-66110b1d8c71", "AppUser", "managed@example.com", true, false, null, "MANAGED@EXAMPLE.COM", "MANAGED", "AQAAAAIAAYagAAAAEMF/gPU+X9QseGw9LnTnXGMKXA+7Z6ydz0WDd6YMcKsVszwACpAE8Ds3r/9Wrf6ZhQ==", 3, null, false, "", 1, false, "managed" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "3", "3" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "3" });

            migrationBuilder.DeleteData(
                table: "Peoples",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Peoples",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Peoples",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Peoples",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peoples",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Peoples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_States_StateId",
                table: "AspNetUsers",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");
        }
    }
}
