using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class priceplane : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceId",
                table: "Company",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PricPlan",
                columns: table => new
                {
                    Priceid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subscriptionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    subscriptionPrice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubscriptionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionPricin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricPlan", x => x.Priceid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_PriceId",
                table: "Company",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_PricPlan_PriceId",
                table: "Company",
                column: "PriceId",
                principalTable: "PricPlan",
                principalColumn: "Priceid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_PricPlan_PriceId",
                table: "Company");

            migrationBuilder.DropTable(
                name: "PricPlan");

            migrationBuilder.DropIndex(
                name: "IX_Company_PriceId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "PriceId",
                table: "Company");
        }
    }
}
