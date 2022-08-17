using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWeekAPI.Migrations
{
    public partial class UpdateAnimalV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Attack", "HP", "Image", "Name" },
                values: new object[] { 9, 1, 5, "https://cdn.discordapp.com/attachments/1008839085172981781/1008892391442350141/monion.png", "Happy Monion" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 9);
        }
    }
}
