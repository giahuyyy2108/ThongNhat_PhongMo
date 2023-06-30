using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "sceensize",
                table: "phongBan",
                type: "nvarchar(max)",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sceensize",
                table: "phongBan");

            
        }
    }
}
