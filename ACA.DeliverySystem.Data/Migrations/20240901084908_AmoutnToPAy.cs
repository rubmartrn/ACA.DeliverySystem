using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ACA.DeliverySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AmoutnToPAy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountToPay",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToPay",
                table: "Orders");
        }
    }
}
