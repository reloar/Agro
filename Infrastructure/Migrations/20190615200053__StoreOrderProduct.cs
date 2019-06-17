using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class _StoreOrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreOrderProducts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    productId = table.Column<long>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    productOwnerId = table.Column<string>(nullable: true),
                    productBuyerId = table.Column<string>(nullable: true),
                    quantityBought = table.Column<long>(nullable: false),
                    currentPriceAtPurchase = table.Column<decimal>(nullable: false),
                    TotalAmountPaid = table.Column<decimal>(nullable: false),
                    priceToSellProduct = table.Column<decimal>(nullable: false),
                    paymentreference = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreOrderProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreOrderProducts");
        }
    }
}
