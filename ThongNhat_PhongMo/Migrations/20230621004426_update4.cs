using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "id_user",
                table: "benhnhan",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_benhnhan_id_user",
                table: "benhnhan",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_benhnhan_Users_id_user",
                table: "benhnhan",
                column: "id_user",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_benhnhan_Users_id_user",
                table: "benhnhan");

            migrationBuilder.DropIndex(
                name: "IX_benhnhan_id_user",
                table: "benhnhan");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "benhnhan");
        }
    }
}
