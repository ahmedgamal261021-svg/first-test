using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class dataContuctus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "interview_date",
                table: "ContactUs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interview_date",
                table: "ContactUs");
        }
    }
}
