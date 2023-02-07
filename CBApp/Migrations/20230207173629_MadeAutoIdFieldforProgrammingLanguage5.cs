using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class MadeAutoIdFieldforProgrammingLanguage5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProgrammingLanguages",
                columns: new[] { "ProgrammingLanguageId", "Name" },
                values: new object[,]
                {
                    { 1, "Ada" },
                    { 2, "Assembly" },
                    { 3, "C" },
                    { 4, "COBOL" },
                    { 5, "CPlusPlus" },
                    { 6, "CSharp" },
                    { 7, "CSS" },
                    { 8, "D" },
                    { 9, "Dart" },
                    { 10, "Erlang" },
                    { 11, "Fortran" },
                    { 12, "FSharp" },
                    { 13, "Go" },
                    { 14, "HTML" },
                    { 15, "Java" },
                    { 16, "JavaScript" },
                    { 17, "Julia" },
                    { 18, "Kotlin" },
                    { 19, "Lisp" },
                    { 20, "Lua" },
                    { 21, "ObjectiveC" },
                    { 22, "Pascal" },
                    { 23, "Perl" },
                    { 24, "PHP" },
                    { 25, "Python" },
                    { 26, "Ruby" },
                    { 27, "Rust" },
                    { 28, "SQL" },
                    { 29, "Swift" },
                    { 30, "Typescript" },
                    { 31, "VisualBasic" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ProgrammingLanguages",
                keyColumn: "ProgrammingLanguageId",
                keyValue: 31);
        }
    }
}
