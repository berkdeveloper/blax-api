using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlaX.CryptoAutoTrading.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentStatusTypeProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatusType",
                table: "UserWallet",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatusType",
                table: "UserWallet");
        }
    }
}
