using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fast_C__Pizza_Co_Back_End.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PizzaOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCost = table.Column<int>(type: "int", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PizzaObj",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PizzaOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaObj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaObj_PizzaOrders_PizzaOrderId",
                        column: x => x.PizzaOrderId,
                        principalTable: "PizzaOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PizzaOrders",
                columns: new[] { "Id", "CreateDate", "DeliveryTime", "TotalCost", "UpdateDate" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 8, 23, 16, 20, 32, 242, DateTimeKind.Local).AddTicks(2999), 36, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "PizzaObj",
                columns: new[] { "Id", "Name", "PizzaOrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Focaccia", 1, 6, 2 },
                    { 3, "Pizza Spinaci", 1, 12, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaObj_PizzaOrderId",
                table: "PizzaObj",
                column: "PizzaOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaObj");

            migrationBuilder.DropTable(
                name: "PizzaOrders");
        }
    }
}
