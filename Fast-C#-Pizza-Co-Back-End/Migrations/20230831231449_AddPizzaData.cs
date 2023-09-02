using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fast_C__Pizza_Co_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class AddPizzaData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PizzaData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ingredients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PhotoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoldOut = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PizzaData",
                columns: new[] { "Id", "Ingredients", "Name", "PhotoName", "Price", "Quantity", "SoldOut" },
                values: new object[,]
                {
                    { 1, "Bread with italian olive oil and rosemary", "Focaccia", "pizzas/focaccia.jpg", 6, 0, false },
                    { 2, "Tomato, basil and mozarella", "Pizza Margherita", "pizzas/margherita.jpg", 10, 0, false },
                    { 3, "Tomato, mozarella, spinach, and ricotta cheese", "Pizza Spinaci", "pizzas/spinaci.jpg", 12, 0, false },
                    { 4, "Tomato, mozarella, mushrooms, and onion", "Pizza Funghi", "pizzas/funghi.jpg", 13, 0, false },
                    { 5, "Tomato, mozarella, ham, aragula, and burrata cheese", "Pizza Prosciutto", "pizzas/prosciutto.jpg", 18, 0, false },
                    { 6, "Tomato, mozarella, mushrooms, and onion", "Pizza Salamino", "pizzas/funghi.jpg", 15, 0, true }
                });

            migrationBuilder.UpdateData(
                table: "PizzaOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryTime",
                value: new DateTime(2023, 9, 1, 11, 14, 48, 920, DateTimeKind.Local).AddTicks(3005));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaData");

            migrationBuilder.UpdateData(
                table: "PizzaOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryTime",
                value: new DateTime(2023, 8, 23, 16, 20, 32, 242, DateTimeKind.Local).AddTicks(2999));
        }
    }
}
