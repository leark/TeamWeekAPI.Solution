using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamWeekAPI.Migrations
{
    public partial class UpdateAnimalV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnimalTeams",
                keyColumn: "AnimalTeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AnimalTeams",
                keyColumn: "AnimalTeamId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AnimalTeams",
                keyColumn: "AnimalTeamId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AnimalTeams",
                keyColumn: "AnimalTeamId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AnimalTeams",
                keyColumn: "AnimalTeamId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Attack", "HP", "Image", "Name" },
                values: new object[] { 6, 4, 2, "https://cdn.discordapp.com/attachments/927592064949026866/1008876043827937350/unknown.png", "Cheeso Dude" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Attack", "HP", "Image", "Name" },
                values: new object[] { 7, 5, 4, "https://cdn.discordapp.com/attachments/927592064949026866/1008873198881869885/73371860-F5C2-4494-AC67-B3EA6111A5D6.jpg", "Cat With Sword" });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Attack", "HP", "Image", "Name" },
                values: new object[] { 8, 3, 3, "https://cdn.discordapp.com/attachments/927592064949026866/1009231673625428058/unknown.png", "Pepper Jackson" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Name" },
                values: new object[,]
                {
                    { 1, "Myrtle" },
                    { 2, "Darrel" },
                    { 3, "Rita" },
                    { 4, "Salvador" },
                    { 5, "Virgil" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Losses", "Name", "PlayerId", "Wins" },
                values: new object[,]
                {
                    { 1, 0, "Militant Commandos", 1, 0 },
                    { 2, 0, "Flash Rockets", 2, 0 },
                    { 3, 0, "Silent Mutants", 3, 0 },
                    { 4, 0, "Nunchuk Killers", 4, 0 },
                    { 5, 0, "Alpha Blasters", 5, 0 }
                });

            migrationBuilder.InsertData(
                table: "AnimalTeams",
                columns: new[] { "AnimalTeamId", "AnimalId", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 },
                    { 5, 5, 5 }
                });
        }
    }
}
