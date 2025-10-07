using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class inter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterviewInformation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    userid = table.Column<int>(type: "int", nullable: false),
                    jobname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    companyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    interview_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contact_number = table.Column<int>(type: "int", nullable: false),
                    interview_address = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    interview_result = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewInformation", x => x.id);
                    table.ForeignKey(
                        name: "FK_InterviewInformation_User_id",
                        column: x => x.id,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterviewInformation");
        }
    }
}
