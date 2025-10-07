using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class PhotoUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_photo",
                table: "Company",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_photo",
                table: "Company");
        }
    }
}
