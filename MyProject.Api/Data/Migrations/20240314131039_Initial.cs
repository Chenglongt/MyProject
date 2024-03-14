using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyProject.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Noodles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noodles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerIdAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoodleOptions",
                columns: table => new
                {
                    NoodleId = table.Column<int>(type: "int", nullable: false),
                    Flavor = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Topping = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoodleOptions", x => new { x.NoodleId, x.Flavor, x.Topping });
                    table.ForeignKey(
                        name: "FK_NoodleOptions_Noodles_NoodleId",
                        column: x => x.NoodleId,
                        principalTable: "Noodles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    NoodleId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Flavor = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Topping = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Noodles",
                columns: new[] { "Id", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Tonkotsu.png", "Tonkotsu", 14.0 },
                    { 2, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Geki%20Kara.png", "Geki Kara", 10.5 },
                    { 3, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Chilli%20Chicken.png", "Chilli Chicken", 13.5 },
                    { 4, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Chilli%20Tiger%20Prawn.png", "Chilli Tiger Prawn", 14.949999999999999 },
                    { 5, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Tokyo.png", "Tokyo ", 14.5 },
                    { 6, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Japanese%20Mushroom%20Miso.png", "Japanese Mushroom Miso", 11.949999999999999 },
                    { 7, "https://github.com/Chenglongt/MyProject/blob/test-and-add-new-attachments/Japanese%20Mushroom.png", "Japanese Mushroom", 11.949999999999999 }
                });

            migrationBuilder.InsertData(
                table: "NoodleOptions",
                columns: new[] { "Flavor", "NoodleId", "Topping" },
                values: new object[,]
                {
                    { "Default", 1, "Chilli Chicken" },
                    { "Default", 1, "Egg" },
                    { "Default", 1, "Pork Belly" },
                    { "Default", 1, "Vegetable" },
                    { "Extra Solt", 1, "Chilli Chicken" },
                    { "Extra Solt", 1, "Egg" },
                    { "Extra Solt", 1, "Pork Belly" },
                    { "Extra Solt", 1, "Vegetable" },
                    { "Extra Spicy", 1, "Chilli Chicken" },
                    { "Extra Spicy", 1, "Egg" },
                    { "Extra Spicy", 1, "Pork Belly" },
                    { "Extra Spicy", 1, "Vegetable" },
                    { "Default", 2, "Chilli Chicken" },
                    { "Default", 2, "Egg" },
                    { "Default", 2, "Pork Belly" },
                    { "Default", 2, "Vegetable" },
                    { "Extra Solt", 2, "Chilli Chicken" },
                    { "Extra Solt", 2, "Egg" },
                    { "Extra Solt", 2, "Pork Belly" },
                    { "Extra Solt", 2, "Vegetable" },
                    { "Extra Spicy", 2, "Chilli Chicken" },
                    { "Extra Spicy", 2, "Egg" },
                    { "Extra Spicy", 2, "Pork Belly" },
                    { "Extra Spicy", 2, "Vegetable" },
                    { "Default", 3, "Chilli Chicken" },
                    { "Default", 3, "Egg" },
                    { "Default", 3, "Pork Belly" },
                    { "Default", 3, "Vegetable" },
                    { "Extra Solt", 3, "Chilli Chicken" },
                    { "Extra Solt", 3, "Egg" },
                    { "Extra Solt", 3, "Pork Belly" },
                    { "Extra Solt", 3, "Vegetable" },
                    { "Extra Spicy", 3, "Chilli Chicken" },
                    { "Extra Spicy", 3, "Egg" },
                    { "Extra Spicy", 3, "Pork Belly" },
                    { "Extra Spicy", 3, "Vegetable" },
                    { "Default", 4, "Chilli Chicken" },
                    { "Default", 4, "Egg" },
                    { "Default", 4, "Pork Belly" },
                    { "Default", 4, "Vegetable" },
                    { "Extra Solt", 4, "Chilli Chicken" },
                    { "Extra Solt", 4, "Egg" },
                    { "Extra Solt", 4, "Pork Belly" },
                    { "Extra Solt", 4, "Vegetable" },
                    { "Extra Spicy", 4, "Chilli Chicken" },
                    { "Extra Spicy", 4, "Egg" },
                    { "Extra Spicy", 4, "Pork Belly" },
                    { "Extra Spicy", 4, "Vegetable" },
                    { "Default", 5, "Chilli Chicken" },
                    { "Default", 5, "Egg" },
                    { "Default", 5, "Pork Belly" },
                    { "Default", 5, "Vegetable" },
                    { "Extra Solt", 5, "Chilli Chicken" },
                    { "Extra Solt", 5, "Egg" },
                    { "Extra Solt", 5, "Pork Belly" },
                    { "Extra Solt", 5, "Vegetable" },
                    { "Extra Spicy", 5, "Chilli Chicken" },
                    { "Extra Spicy", 5, "Egg" },
                    { "Extra Spicy", 5, "Pork Belly" },
                    { "Extra Spicy", 5, "Vegetable" },
                    { "Default", 6, "Chilli Chicken" },
                    { "Default", 6, "Egg" },
                    { "Default", 6, "Pork Belly" },
                    { "Default", 6, "Vegetable" },
                    { "Extra Solt", 6, "Chilli Chicken" },
                    { "Extra Solt", 6, "Egg" },
                    { "Extra Solt", 6, "Pork Belly" },
                    { "Extra Solt", 6, "Vegetable" },
                    { "Extra Spicy", 6, "Chilli Chicken" },
                    { "Extra Spicy", 6, "Egg" },
                    { "Extra Spicy", 6, "Pork Belly" },
                    { "Extra Spicy", 6, "Vegetable" },
                    { "Default", 7, "Chilli Chicken" },
                    { "Default", 7, "Egg" },
                    { "Default", 7, "Pork Belly" },
                    { "Default", 7, "Vegetable" },
                    { "Extra Solt", 7, "Chilli Chicken" },
                    { "Extra Solt", 7, "Egg" },
                    { "Extra Solt", 7, "Pork Belly" },
                    { "Extra Solt", 7, "Vegetable" },
                    { "Extra Spicy", 7, "Chilli Chicken" },
                    { "Extra Spicy", 7, "Egg" },
                    { "Extra Spicy", 7, "Pork Belly" },
                    { "Extra Spicy", 7, "Vegetable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoodleOptions");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Noodles");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
