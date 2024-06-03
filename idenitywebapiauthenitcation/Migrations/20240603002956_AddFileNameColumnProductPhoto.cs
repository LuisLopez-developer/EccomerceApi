using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFileNameColumnProductPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ProductPhotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4c1e81bb-8964-4911-b778-4046bbb9a844", "AQAAAAIAAYagAAAAEIvum5URveyrN4ioYE0/qixvnolonKVvoS4c/pfik5Oa6sVgflDFuLDnh0HdyaO6oA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f0997ee-b0e3-45d8-8672-44ee83c7edee", "AQAAAAIAAYagAAAAEECaiHhho39CjWSzfy26ryklkEDRoHc4cDL9l5xXZtD89W3ieWiZsjREpNwupkxBGQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c450b469-69df-4999-a61f-ac05e94dfeb3", "AQAAAAIAAYagAAAAEGRsQpVH18kKOFm4aBT0jb8M6ZZvCy61RFctiqTf/Dgyy9l4g0tGjkv3W7H8wbYBNg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ProductPhotos");

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
        }
    }
}
