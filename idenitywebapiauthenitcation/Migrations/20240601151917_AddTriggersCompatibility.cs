using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggersCompatibility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_States_StateId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c1dec792-eb28-41ed-899c-264d3f85764c", "AQAAAAIAAYagAAAAEBGMHXShKGsBZaJGBfpI6nWqY/Ez/ORn7OhTmVhpC81oYKJVMj+P8KHIOPat2W+gRQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "33e1395c-3c45-4875-a749-c8cf3e89cbe8", "AQAAAAIAAYagAAAAEAwBq5xPkIF/aqic7LuOZyt0QP8USPTbR9tVQTR8iFZJR3XPiaAM+L/2GTfR3xPc4w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f853a160-08ae-4aed-96a1-2ff7d5c83522", "AQAAAAIAAYagAAAAEKwUMn/IXGKc/xEFS+T+Omjb4L3cVWqMgR9tpuyFmMfFCtVcZIj8SCD5XqeK7Wi7XQ==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_States_StateId",
                table: "Products",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_States_StateId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c20d095-9d33-442e-bcfd-24b2e7118ab3", "AQAAAAIAAYagAAAAENsGaS33fOeRY6uAzwKASciER2FdoN98iOfMgdvq0+bnWawC3ZnEe2hPKJiHqJj00w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "660b5030-379f-410c-8301-28a1cf538a8a", "AQAAAAIAAYagAAAAEEaZ+VAGpHhh3GPPwt2hO6PwYHC7usx6nboBV1S4cmuFvJ3ZmO7SRaVmPTDdFXfdLg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d6d4e8b-4d55-4bf5-8809-fab418ab9263", "AQAAAAIAAYagAAAAEDVNdikGhBp4RHpG6AZjc69q/gtJPnkuAuYTrHO4Wx1vamHH1YJwoAdBn841nMmUjw==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_States_StateId",
                table: "Products",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id");
        }
    }
}
