using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EccomerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedersGeneral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8fbf8c4e-8ba8-4b3b-b3b4-b9172961eef0", "AQAAAAIAAYagAAAAELheiaodFTcb02grEBn/5gmXf8vkWpvvv6bA+lQfxKBkDoeYu0udcbJiCSO198seMg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a083ed3e-47d4-4c44-b72c-c681b1c459e4", "AQAAAAIAAYagAAAAEHfW4ACAdFEcNhTSvSSpraepnaxLj9H2vksIs0K33uB60DFHdPOZ/FMnD+hvuY4WQQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "03fa7cc5-f796-42cc-acf0-8de6e846779a", "AQAAAAIAAYagAAAAEMcYqw1OwsN1XdHdrb8I3P6VLyeux1ibTkne8lRMHWpi+Ri+Iw5DMpVVOQ9N4U170g==" });

            migrationBuilder.InsertData(
                table: "EntryType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Compra" },
                    { 2, "Transferencia de inventario" },
                    { 3, "Devolución de cliente" },
                    { 4, "Donación recibida" },
                    { 5, "Muestra gratuita" }
                });

            migrationBuilder.InsertData(
                table: "LossReasons",
                columns: new[] { "Id", "Reason" },
                values: new object[,]
                {
                    { 1, "Daño durante el transporte" },
                    { 2, "Fecha de caducidad vencida" },
                    { 3, "Robo" },
                    { 4, "Producto dañado en el almacén" },
                    { 5, "Devolución del cliente" },
                    { 6, "Muestra gratuita" }
                });

            migrationBuilder.InsertData(
                table: "ProductBrands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Apple" },
                    { 3, "Huawei" },
                    { 4, "Xiaomi" },
                    { 5, "Motorola" },
                    { 6, "Lg" },
                    { 7, "Sony" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Gama Baja" },
                    { 2, "Gama Media" },
                    { 3, "Gama Alta" },
                    { 4, "Gama Top" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EntryType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EntryType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EntryType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EntryType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EntryType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LossReasons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProductBrands",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d65ec53a-2020-413c-8994-c65d87c6148c", "AQAAAAIAAYagAAAAEMuMkP5/t6MHfGE8v1Zj5staqUcheQHcNHqlrg6qs600c9PG51X3FkYRL61NIqd9gw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35c56708-a162-47f6-a9bb-a9849d77c6d4", "AQAAAAIAAYagAAAAED9dohuh5NaTLZECxAtQL9wWeDTcWuGxTpOl+Kt+ChzfH7nJCbfw9Sp4baFLb3RDCg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "882b9c8f-d920-4a00-b45b-66110b1d8c71", "AQAAAAIAAYagAAAAEMF/gPU+X9QseGw9LnTnXGMKXA+7Z6ydz0WDd6YMcKsVszwACpAE8Ds3r/9Wrf6ZhQ==" });
        }
    }
}
