using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recidol.Migrations
{
    /// <inheritdoc />
    public partial class newModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lineItems",
                columns: table => new
                {
                    receiptId = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    quantity = table.Column<double>(type: "REAL", nullable: true),
                    totalAmount = table.Column<double>(type: "REAL", nullable: true),
                    price = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "receiptInfo",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    userId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierName = table.Column<string>(type: "TEXT", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: false),
                    totalAmount = table.Column<double>(type: "REAL", nullable: false),
                    date = table.Column<string>(type: "TEXT", nullable: false),
                    time = table.Column<string>(type: "TEXT", nullable: false),
                    imagePath = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_receiptInfo", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lineItems");

            migrationBuilder.DropTable(
                name: "receiptInfo");
        }
    }
}
