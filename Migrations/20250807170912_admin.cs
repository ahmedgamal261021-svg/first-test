using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace first_test.Migrations
{
    public partial class admin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_InterviewInformation_User_id",
            //    table: "InterviewInformation");

            //migrationBuilder.AlterColumn<int>(
            //    name: "id",
            //    table: "InterviewInformation",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "Rigesteradmin",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_num = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rigesteradmin", x => x.AdminId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterviewInformation_userid",
                table: "InterviewInformation",
                column: "userid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewInformation_User_userid",
                table: "InterviewInformation",
                column: "userid",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterviewInformation_User_userid",
                table: "InterviewInformation");

            migrationBuilder.DropTable(
                name: "Rigesteradmin");

            migrationBuilder.DropIndex(
                name: "IX_InterviewInformation_userid",
                table: "InterviewInformation");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "InterviewInformation",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewInformation_User_id",
                table: "InterviewInformation",
                column: "id",
                principalTable: "User",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
