using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class ReplacedUnderscoresInCSInterestNamesWithSpaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 1,
                column: "Name",
                value: "Artificial Intelligence");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 3,
                column: "Name",
                value: "Database Technology");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 5,
                column: "Name",
                value: "Games Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 6,
                column: "Name",
                value: "Graphics Programming");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 7,
                column: "Name",
                value: "Intelligent Signal Processing");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 8,
                column: "Name",
                value: "Internet of Things");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 9,
                column: "Name",
                value: "Machine Learning");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 10,
                column: "Name",
                value: "Mobile Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 11,
                column: "Name",
                value: "Neural Networks");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 12,
                column: "Name",
                value: "Natural Language Processing");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 13,
                column: "Name",
                value: "Theoretical Computer Science");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 14,
                column: "Name",
                value: "Web Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 15,
                column: "Name",
                value: "User Experience");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 16,
                column: "Name",
                value: "Virtual Reality");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 1,
                column: "Name",
                value: "Artificial_Intelligence");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 3,
                column: "Name",
                value: "Database_Technology");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 5,
                column: "Name",
                value: "Games_Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 6,
                column: "Name",
                value: "Graphics_Programming");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 7,
                column: "Name",
                value: "Intelligent_Signal_Processing");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 8,
                column: "Name",
                value: "Internet_of_Things");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 9,
                column: "Name",
                value: "Machine_Learning");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 10,
                column: "Name",
                value: "Mobile_Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 11,
                column: "Name",
                value: "Neural_Networks");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 12,
                column: "Name",
                value: "Natural_Language_Processing");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 13,
                column: "Name",
                value: "Theoretical_Computer_Science");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 14,
                column: "Name",
                value: "Web_Development");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 15,
                column: "Name",
                value: "User_Experience");

            migrationBuilder.UpdateData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 16,
                column: "Name",
                value: "Virtual_Reality");
        }
    }
}
