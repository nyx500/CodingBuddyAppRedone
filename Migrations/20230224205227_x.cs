using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class x : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3,
                column: "QuestionString",
                value: "What did you want to be when you were a kid?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5,
                column: "QuestionString",
                value: "If you were a super-hero, what one power would you have?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 10,
                column: "QuestionString",
                value: "Which three things do you think of the most each day?");

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "QuestionString" },
                values: new object[,]
                {
                    { 11, "What is your favourite song?" },
                    { 12, "Who is your role model?" },
                    { 13, "What's your most hated form of transportation?" },
                    { 14, "What part of a kid’s movie completely scarred you for life?" },
                    { 15, "What's the most disgusting food you've ever eaten?" },
                    { 16, "Do you like tacos?" },
                    { 17, "What do you think would make an awesome name for a Wi-Fi network?" },
                    { 18, "If you could buy any computer regardless of cost, which one would you go for?" },
                    { 19, "What is better: C# or Java?" },
                    { 20, "What is the most annoying bug you ever found when coding?" },
                    { 21, "Which conspiracy theory do you find particularly ridiculous?" },
                    { 22, "Is Starbucks worth the money?" },
                    { 23, "Type an unpopular opinion you have." },
                    { 24, "Do you know your personality type?" },
                    { 25, "What do you like to do best to relax?" },
                    { 26, "Name one of your guilty pleasures." },
                    { 27, "What is your favourite outfit?" },
                    { 28, "What is the first thing you would buy if you won the lottery?" },
                    { 29, "What is something everyone seems to love that you hate?" },
                    { 30, "What is your favourite animal?" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 3,
                column: "QuestionString",
                value: "What did you want to be when you were small?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 5,
                column: "QuestionString",
                value: "If you were a super-hero, what powers would you have?");

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: 10,
                column: "QuestionString",
                value: "What three things do you think of the most each day?");
        }
    }
}
