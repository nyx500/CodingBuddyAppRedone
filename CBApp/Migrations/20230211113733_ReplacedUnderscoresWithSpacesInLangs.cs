using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class ReplacedUnderscoresWithSpacesInLangs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 20,
                column: "Name",
                value: "Eastern Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 24,
                column: "Name",
                value: "Gan Chinese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 28,
                column: "Name",
                value: "Haitian Creole");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 54,
                column: "Name",
                value: "Malay Indonesian");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 61,
                column: "Name",
                value: "Northern Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 62,
                column: "Name",
                value: "Odia Oriya");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 73,
                column: "Name",
                value: "Serbo Croatian");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 78,
                column: "Name",
                value: "Southern Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 94,
                column: "Name",
                value: "Wu inc Shanghainese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 96,
                column: "Name",
                value: "Xiang Hunnanese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 98,
                column: "Name",
                value: "Yue Cantonese");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 20,
                column: "Name",
                value: "Eastern_Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 24,
                column: "Name",
                value: "Gan_Chinese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 28,
                column: "Name",
                value: "Haitian_Creole");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 54,
                column: "Name",
                value: "Malay_Indonesian");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 61,
                column: "Name",
                value: "Northern_Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 62,
                column: "Name",
                value: "Odia_Oriya");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 73,
                column: "Name",
                value: "Serbo_Croatian");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 78,
                column: "Name",
                value: "Southern_Min");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 94,
                column: "Name",
                value: "Wu_inc_Shanghainese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 96,
                column: "Name",
                value: "Xiang_Hunnanese");

            migrationBuilder.UpdateData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 98,
                column: "Name",
                value: "Yue_Cantonese");
        }
    }
}
