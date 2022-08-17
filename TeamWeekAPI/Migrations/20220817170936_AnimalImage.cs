using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWeekAPI.Migrations
{
    public partial class AnimalImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Animals",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 1,
                column: "Image",
                value: "https://cdn.discordapp.com/attachments/1008839085172981781/1008883732104626246/musclepikachu.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 2,
                column: "Image",
                value: "https://cdn.discordapp.com/attachments/1008839085172981781/1008930285691351131/MonionNoBgZoom.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 3,
                column: "Image",
                value: "https://cdn.discordapp.com/attachments/1008839085172981781/1009168139780624395/chickenyoshi.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 4,
                column: "Image",
                value: "https://cdn.discordapp.com/attachments/1008839085172981781/1009185539242606743/beerbellybear.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 5,
                column: "Image",
                value: "https://cdn.discordapp.com/attachments/1008839085172981781/1009201332357439619/peterpigeon.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Animals");
        }
    }
}
