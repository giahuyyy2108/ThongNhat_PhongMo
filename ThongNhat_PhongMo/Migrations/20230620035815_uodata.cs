using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class uodata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_benhnhan_phongBan_id_phongban",
                table: "benhnhan");

            

            migrationBuilder.AlterColumn<int>(
                name: "id_phongban",
                table: "benhnhan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "hoten",
                table: "benhnhan",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_benhnhan_phongBan_id_phongban",
                table: "benhnhan",
                column: "id_phongban",
                principalTable: "phongBan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_benhnhan_phongBan_id_phongban",
                table: "benhnhan");

          

            migrationBuilder.AlterColumn<int>(
                name: "id_phongban",
                table: "benhnhan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "hoten",
                table: "benhnhan",
                type: "nvarchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_benhnhan_phongBan_id_phongban",
                table: "benhnhan",
                column: "id_phongban",
                principalTable: "phongBan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
