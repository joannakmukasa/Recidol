using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recidol.Migrations
{
    /// <inheritdoc />
    public partial class itemsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "lineItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id",
                table: "lineItems");
        }
    }
}
