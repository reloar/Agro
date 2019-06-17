using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class UsingOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreOrderProducts");

            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "StorePrice",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Order",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "TransactionFee",
                table: "Order",
                newName: "currentPriceAtPurchase");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Order",
                newName: "TotalAmountPaid");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Order",
                newName: "quantityBought");

            migrationBuilder.RenameColumn(
                name: "CurrentuserId",
                table: "Order",
                newName: "reference");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "Order",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "buyerContact",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "callbackUrl",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deliveryAddress",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productBuyerId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "productOwnerId",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "buyerContact",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "callbackUrl",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "deliveryAddress",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "productBuyerId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "productOwnerId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "Order",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "reference",
                table: "Order",
                newName: "CurrentuserId");

            migrationBuilder.RenameColumn(
                name: "quantityBought",
                table: "Order",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "currentPriceAtPurchase",
                table: "Order",
                newName: "TransactionFee");

            migrationBuilder.RenameColumn(
                name: "TotalAmountPaid",
                table: "Order",
                newName: "Total");

            migrationBuilder.AddColumn<decimal>(
                name: "IsSuccessful",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "StorePrice",
                table: "Order",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "StoreOrderProducts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<long>(nullable: false),
                    TotalAmountPaid = table.Column<decimal>(nullable: false),
                    currentPriceAtPurchase = table.Column<decimal>(nullable: false),
                    paymentreference = table.Column<string>(nullable: true),
                    priceToSellProduct = table.Column<decimal>(nullable: false),
                    productBuyerId = table.Column<string>(nullable: true),
                    productId = table.Column<long>(nullable: false),
                    productOwnerId = table.Column<string>(nullable: true),
                    quantityBought = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrderProducts", x => x.Id);
                });
        }
    }
}
