using Microsoft.EntityFrameworkCore.Migrations;

namespace ThongNhat_PhongMo.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1 ; i < 10; i++)
            {
                migrationBuilder.InsertData(
                    "phongBan",
                    columns: new[]
                    {
                        "name"
                    },
                    values: new object[]
                    {
                        $"Phòng mổ {i}"
                    });
            }

            for (int i = 1; i < 5; i++)
            {
                migrationBuilder.InsertData(
                    "tinhtrang",
                    columns: new[]
                    {
                        "Name"
                    },
                    values: new object[]
                    {
                        $"Tình trạng {i}"
                    });
            }

            migrationBuilder.InsertData(
                    "tinhtrang",
                    columns: new[]
                    {
                        "Name"
                    },
                    values: new object[]
                    {
                        $"Done"
                    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
