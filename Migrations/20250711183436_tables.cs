using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emp_fname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_lname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    emp_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    job_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    government = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    field_of_work = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Company_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_num = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    age = table.Column<int>(type: "int", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    job_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    government = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    education_level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    experience_level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    job_field = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    government = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    type_of_job = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    number_of_employees = table.Column<int>(type: "int", nullable: false),
                    job_details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    educational_level = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    years_of_experience = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    salary_min = table.Column<int>(type: "int", nullable: false),
                    salary_max = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_job_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userwithjob",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<int>(type: "int", nullable: false),
                    jobid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userwithjob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userwithjob_job_jobid",
                        column: x => x.jobid,
                        principalTable: "job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userwithjob_User_Userid",
                        column: x => x.Userid,
                        principalTable: "User",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_job_CompanyId",
                table: "job",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_userwithjob_jobid",
                table: "userwithjob",
                column: "jobid");

            migrationBuilder.CreateIndex(
                name: "IX_userwithjob_Userid",
                table: "userwithjob",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userwithjob");

            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
