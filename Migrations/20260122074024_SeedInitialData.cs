using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Electronic devices and accessories", "Electronics", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Printed and electronic books", "Books", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Small and large home appliances", "Home Appliances", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Men and women apparel", "Clothing", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Toys and games for children", "Toys", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Sporting goods and accessories", "Sports", new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "15 inch laptop", "Laptop", 999.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 1, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Android smartphone", "Smartphone", 499.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Fiction novel", "Novel", 19.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Healthy recipes", "Cookbook", 29.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 2, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Life story", "Biography", 14.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "High-power blender", "Blender", 89.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 3, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Bagless vacuum", "Vacuum Cleaner", 149.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 4, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Cotton T-Shirt", "T-Shirt", 19.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 4, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Denim jeans", "Jeans", 49.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 4, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Waterproof jacket", "Jacket", 89.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 5, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Educational blocks", "Building Blocks", 24.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 5, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Remote controlled car", "Remote Car", 39.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 6, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Official size football", "Football", 29.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 6, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Lightweight racket", "Tennis Racket", 79.99m, new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
