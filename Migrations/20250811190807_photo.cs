using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Priceid",
                table: "PricPlan",
                newName: "PriceId");

            migrationBuilder.AddColumn<string>(
                name: "profile_photo",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "subscriptionPrice",
                table: "PricPlan",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_photo",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "PricPlan",
                newName: "Priceid");

            migrationBuilder.AlterColumn<string>(
                name: "subscriptionPrice",
                table: "PricPlan",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
