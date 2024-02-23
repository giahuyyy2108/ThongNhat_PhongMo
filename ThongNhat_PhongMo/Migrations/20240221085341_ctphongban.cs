using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class ctphongban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CT_PhongBan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_User = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    id_phongban = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CT_PhongBan", x => x.id);
                    table.ForeignKey(
                        name: "FK_CT_PhongBan_phongBan_id_phongban",
                        column: x => x.id_phongban,
                        principalTable: "phongBan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CT_PhongBan_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhongBan_id_phongban",
                table: "CT_PhongBan",
                column: "id_phongban");

            migrationBuilder.CreateIndex(
                name: "IX_CT_PhongBan_Id_User",
                table: "CT_PhongBan",
                column: "Id_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_PhongBan");
        }
    }
}
