using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class CreateCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CreateCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateCategory", x => x.CategoryId);
                });
            migrationBuilder.Sql("INSERT INTO CreateCategory (CategoryName) VALUES (N'غير مصنف')");
            migrationBuilder.Sql("UPDATE job SET CategoryId = 1 WHERE CategoryId = 0");


            migrationBuilder.CreateIndex(
                name: "IX_job_CategoryId",
                table: "job",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_job_CreateCategory_CategoryId",
                table: "job",
                column: "CategoryId",
                principalTable: "CreateCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_job_CreateCategory_CategoryId",
                table: "job");

            migrationBuilder.DropTable(
                name: "CreateCategory");

            migrationBuilder.DropIndex(
                name: "IX_job_CategoryId",
                table: "job");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "job");
        }
    }
}
