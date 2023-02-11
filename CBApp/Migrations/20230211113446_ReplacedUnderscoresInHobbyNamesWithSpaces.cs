using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class ReplacedUnderscoresInHobbyNamesWithSpaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "Name",
                value: "Adventure Sports");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 12,
                column: "Name",
                value: "Board Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 13,
                column: "Name",
                value: "Card Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 17,
                column: "Name",
                value: "Classical Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 23,
                column: "Name",
                value: "Creative Writing");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 25,
                column: "Name",
                value: "Digital Art");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 30,
                column: "Name",
                value: "Electronic Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 36,
                column: "Name",
                value: "Folk Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 43,
                column: "Name",
                value: "Horseback Riding");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 45,
                column: "Name",
                value: "Learning Languages");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 47,
                column: "Name",
                value: "Martial Arts");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 51,
                column: "Name",
                value: "Music Production");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 52,
                column: "Name",
                value: "Non Fiction");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 58,
                column: "Name",
                value: "Playing An Instrument");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 60,
                column: "Name",
                value: "Pop Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 63,
                column: "Name",
                value: "Rock Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 71,
                column: "Name",
                value: "Snow Sports");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 72,
                column: "Name",
                value: "Street Art");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 77,
                column: "Name",
                value: "Video Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 80,
                column: "Name",
                value: "Water Sports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2,
                column: "Name",
                value: "Adventure_Sports");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 12,
                column: "Name",
                value: "Board_Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 13,
                column: "Name",
                value: "Card_Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 17,
                column: "Name",
                value: "Classical_Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 23,
                column: "Name",
                value: "Creative_Writing");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 25,
                column: "Name",
                value: "Digital_Art");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 30,
                column: "Name",
                value: "Electronic_Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 36,
                column: "Name",
                value: "Folk_Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 43,
                column: "Name",
                value: "Horseback_Riding");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 45,
                column: "Name",
                value: "Learning_Languages");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 47,
                column: "Name",
                value: "Martial_Arts");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 51,
                column: "Name",
                value: "Music_Production");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 52,
                column: "Name",
                value: "Non_Fiction");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 58,
                column: "Name",
                value: "Playing_An_Instrument");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 60,
                column: "Name",
                value: "Pop_Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 63,
                column: "Name",
                value: "Rock_Music");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 71,
                column: "Name",
                value: "Snow_Sports");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 72,
                column: "Name",
                value: "Street_Art");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 77,
                column: "Name",
                value: "Video_Games");

            migrationBuilder.UpdateData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 80,
                column: "Name",
                value: "Water_Sports");
        }
    }
}
