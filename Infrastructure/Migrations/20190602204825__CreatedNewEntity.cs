using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class _CreatedNewEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Order",
                newName: "TransactionFee");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "CurrentuserId");

            migrationBuilder.RenameColumn(
                name: "AmountPaid",
                table: "Order",
                newName: "StorePrice");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Order",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Order",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Accountname",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Accountname",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TransactionFee",
                table: "Order",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "StorePrice",
                table: "Order",
                newName: "AmountPaid");

            migrationBuilder.RenameColumn(
                name: "CurrentuserId",
                table: "Order",
                newName: "OrderId");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
