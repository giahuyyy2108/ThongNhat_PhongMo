using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "phongBan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phongBan", x => x.Id);
                });

            

            migrationBuilder.CreateTable(
                name: "tinhtrang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tinhtrang", x => x.id);
                });

            

            migrationBuilder.CreateTable(
                name: "benhnhan",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    hoten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    namsinh = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    mabn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_tinhtrang = table.Column<int>(type: "int", nullable: false),
                    id_phongban = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_benhnhan", x => x.id);
                    table.ForeignKey(
                        name: "FK_benhnhan_phongBan_id_phongban",
                        column: x => x.id_phongban,
                        principalTable: "phongBan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_benhnhan_tinhtrang_id_tinhtrang",
                        column: x => x.id_tinhtrang,
                        principalTable: "tinhtrang",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_benhnhan_id_phongban",
                table: "benhnhan",
                column: "id_phongban");

            migrationBuilder.CreateIndex(
                name: "IX_benhnhan_id_tinhtrang",
                table: "benhnhan",
                column: "id_tinhtrang");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "benhnhan");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "phongBan");

            migrationBuilder.DropTable(
                name: "tinhtrang");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
