using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class Seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CareerPhase_CareerPhaseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ExperienceLevel_ExperienceLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceLevel",
                table: "ExperienceLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareerPhase",
                table: "CareerPhase");

            migrationBuilder.RenameTable(
                name: "Gender",
                newName: "Genders");

            migrationBuilder.RenameTable(
                name: "ExperienceLevel",
                newName: "ExperienceLevels");

            migrationBuilder.RenameTable(
                name: "CareerPhase",
                newName: "CareerPhases");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceLevels",
                table: "ExperienceLevels",
                column: "ExperienceLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareerPhases",
                table: "CareerPhases",
                column: "CareerPhaseId");

            migrationBuilder.InsertData(
                table: "CSInterests",
                columns: new[] { "CSInterestId", "Name" },
                values: new object[,]
                {
                    { 1, "Artificial_Intelligence" },
                    { 2, "Cybersecurity" },
                    { 3, "Database_Technology" },
                    { 4, "Fintech" },
                    { 5, "Games_Development" },
                    { 6, "Graphics_Programming" },
                    { 7, "Intelligent_Signal_Processing" },
                    { 8, "Internet_of_Things" },
                    { 9, "Machine_Learning" },
                    { 10, "Mobile_Development" },
                    { 11, "Neural_Networks" },
                    { 12, "Natural_Language_Processing" },
                    { 13, "Theoretical_Computer_Science" },
                    { 14, "Web_Development" },
                    { 15, "User_Experience" },
                    { 16, "Virtual_Reality" }
                });

            migrationBuilder.InsertData(
                table: "CareerPhases",
                columns: new[] { "CareerPhaseId", "Name" },
                values: new object[,]
                {
                    { 1, "Starter" },
                    { 2, "Changer" },
                    { 3, "Developer" }
                });

            migrationBuilder.InsertData(
                table: "ExperienceLevels",
                columns: new[] { "ExperienceLevelId", "Name" },
                values: new object[,]
                {
                    { 1, "Beginner" },
                    { 2, "Novice" },
                    { 3, "Intermediate" },
                    { 4, "Advanced" },
                    { 5, "Expert" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "GenderId", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "HobbyId", "Name" },
                values: new object[,]
                {
                    { 1, "Acting" },
                    { 2, "Adventure_Sports" },
                    { 3, "Anime" },
                    { 4, "Archery" },
                    { 5, "Art" },
                    { 6, "Astrology" },
                    { 7, "Astronomy" },
                    { 8, "Baseball" },
                    { 9, "Basketball" },
                    { 10, "Biking" },
                    { 11, "Blogging" },
                    { 12, "Board_Games" },
                    { 13, "Card_Games" },
                    { 14, "Cars" },
                    { 15, "Chess" },
                    { 16, "Cinema" },
                    { 17, "Classical_Music" },
                    { 18, "Comics" },
                    { 19, "Concerts" },
                    { 20, "Cooking" },
                    { 21, "Cosplaying" },
                    { 22, "Crafts" },
                    { 23, "Creative_Writing" },
                    { 24, "Cycling" },
                    { 25, "Digital_Art" },
                    { 26, "DIY" },
                    { 27, "DJing" },
                    { 28, "Drawing" },
                    { 29, "Economics" },
                    { 30, "Electronic_Music" },
                    { 31, "Entrepreneurship" },
                    { 32, "Fashion" },
                    { 33, "Fiction" },
                    { 34, "Filmmaking" },
                    { 35, "Fitness" },
                    { 36, "Folk_Music" },
                    { 37, "Football" },
                    { 38, "Golf" },
                    { 39, "Handball" },
                    { 40, "Hiking" },
                    { 41, "History" },
                    { 42, "Hockey" },
                    { 43, "Horseback_Riding" },
                    { 44, "Jazz" },
                    { 45, "Learning_Languages" },
                    { 46, "Mathematics" },
                    { 47, "Martial_Arts" },
                    { 48, "Metal" },
                    { 49, "Music" },
                    { 50, "Musicals" },
                    { 51, "Music_Production" },
                    { 52, "Non_Fiction" },
                    { 53, "Opera" },
                    { 54, "Painting" },
                    { 55, "Pets" },
                    { 56, "Philosophy" },
                    { 57, "Photography" },
                    { 58, "Playing_An_Instrument" },
                    { 59, "Politics" },
                    { 60, "Pop_Music" },
                    { 61, "Reading" },
                    { 62, "Restaurants" },
                    { 63, "Rock_Music" },
                    { 64, "Rugby" },
                    { 65, "Running" },
                    { 66, "Sailing" },
                    { 67, "Sculpting" },
                    { 68, "Sewing" },
                    { 69, "Shopping" },
                    { 70, "Skiing" },
                    { 71, "Snow_Sports" },
                    { 72, "Street_Art" },
                    { 73, "Swimming" },
                    { 74, "Tennis" },
                    { 75, "Theatre" },
                    { 76, "Travel" },
                    { 77, "Video_Games" },
                    { 78, "Volunteering" },
                    { 79, "Walking" },
                    { 80, "Water_Sports" },
                    { 81, "Yoga" }
                });

            migrationBuilder.InsertData(
                table: "NaturalLanguages",
                columns: new[] { "NaturalLanguageId", "Name" },
                values: new object[,]
                {
                    { 1, "Akan" },
                    { 2, "Amharic" },
                    { 3, "Arabic" },
                    { 4, "Assamese" },
                    { 5, "Awadhi" },
                    { 6, "Azerbaijani" },
                    { 7, "Balochi" },
                    { 8, "Belarusian" },
                    { 9, "Bengali" },
                    { 10, "Bhojpuri" },
                    { 11, "Burmese" },
                    { 12, "Cebuano" },
                    { 13, "Chewa" },
                    { 14, "Chhattisgarhi" },
                    { 15, "Chittagonian" },
                    { 16, "Czech" },
                    { 17, "Deccan" },
                    { 18, "Dhundhari" },
                    { 19, "Dutch" },
                    { 20, "Eastern_Min" },
                    { 21, "English" },
                    { 22, "French" },
                    { 23, "Fula" },
                    { 24, "Gan_Chinese" },
                    { 25, "German" },
                    { 26, "Greek" },
                    { 27, "Gujarati" },
                    { 28, "Haitian_Creole" },
                    { 29, "Hakka" },
                    { 30, "Haryanvi" },
                    { 31, "Hausa" },
                    { 32, "Hiligaynon" },
                    { 33, "Hindi" },
                    { 34, "Hmong" },
                    { 35, "Hungarian" },
                    { 36, "Igbo" },
                    { 37, "Ilocano" },
                    { 38, "Italian" },
                    { 39, "Japanese" },
                    { 40, "Javanese" },
                    { 41, "Jin" },
                    { 42, "Kannada" },
                    { 43, "Kazakh" },
                    { 44, "Khmer" },
                    { 45, "Kinyarwanda" },
                    { 46, "Kirundi" },
                    { 47, "Konkani" },
                    { 48, "Korean" },
                    { 49, "Kurdish" },
                    { 50, "Madurese" },
                    { 51, "Magahi" },
                    { 52, "Maithili" },
                    { 53, "Malagasy" },
                    { 54, "Malay_Indonesian" },
                    { 55, "Malayalam" },
                    { 56, "Mandarin" },
                    { 57, "Marathi" },
                    { 58, "Marwari" },
                    { 59, "Mossi" },
                    { 60, "Nepali" },
                    { 61, "Northern_Min" },
                    { 62, "Odia_Oriya" },
                    { 63, "Oromo" },
                    { 64, "Pashto" },
                    { 65, "Persian" },
                    { 66, "Polish" },
                    { 67, "Portuguese" },
                    { 68, "Punjabi" },
                    { 69, "Quechua" },
                    { 70, "Romanian" },
                    { 71, "Russian" },
                    { 72, "Saraiki" },
                    { 73, "Serbo_Croatian" },
                    { 74, "Shona" },
                    { 75, "Sindhi" },
                    { 76, "Sinhalese" },
                    { 77, "Somali" },
                    { 78, "Southern_Min" },
                    { 79, "Spanish" },
                    { 80, "Sundanese" },
                    { 81, "Swedish" },
                    { 82, "Sylheti" },
                    { 83, "Tagalog" },
                    { 84, "Tamil" },
                    { 85, "Telugu" },
                    { 86, "Thai" },
                    { 87, "Turkish" },
                    { 88, "Turkmen" },
                    { 89, "Ukrainian" },
                    { 90, "Urdu" },
                    { 91, "Uyghur" },
                    { 92, "Uzbek" },
                    { 93, "Vietnamese" },
                    { 94, "Wu_inc_Shanghainese" },
                    { 95, "Xhosa" },
                    { 96, "Xiang_Hunnanese" },
                    { 97, "Yoruba" },
                    { 98, "Yue_Cantonese" },
                    { 99, "Zhuang" },
                    { 100, "Zulu" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CareerPhases_CareerPhaseId",
                table: "AspNetUsers",
                column: "CareerPhaseId",
                principalTable: "CareerPhases",
                principalColumn: "CareerPhaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ExperienceLevels_ExperienceLevelId",
                table: "AspNetUsers",
                column: "ExperienceLevelId",
                principalTable: "ExperienceLevels",
                principalColumn: "ExperienceLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CareerPhases_CareerPhaseId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ExperienceLevels_ExperienceLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExperienceLevels",
                table: "ExperienceLevels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CareerPhases",
                table: "CareerPhases");

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CSInterests",
                keyColumn: "CSInterestId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "CareerPhases",
                keyColumn: "CareerPhaseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CareerPhases",
                keyColumn: "CareerPhaseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CareerPhases",
                keyColumn: "CareerPhaseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExperienceLevels",
                keyColumn: "ExperienceLevelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExperienceLevels",
                keyColumn: "ExperienceLevelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExperienceLevels",
                keyColumn: "ExperienceLevelId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExperienceLevels",
                keyColumn: "ExperienceLevelId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExperienceLevels",
                keyColumn: "ExperienceLevelId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "GenderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Hobbies",
                keyColumn: "HobbyId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "NaturalLanguages",
                keyColumn: "NaturalLanguageId",
                keyValue: 100);

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "Gender");

            migrationBuilder.RenameTable(
                name: "ExperienceLevels",
                newName: "ExperienceLevel");

            migrationBuilder.RenameTable(
                name: "CareerPhases",
                newName: "CareerPhase");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "GenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExperienceLevel",
                table: "ExperienceLevel",
                column: "ExperienceLevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CareerPhase",
                table: "CareerPhase",
                column: "CareerPhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CareerPhase_CareerPhaseId",
                table: "AspNetUsers",
                column: "CareerPhaseId",
                principalTable: "CareerPhase",
                principalColumn: "CareerPhaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ExperienceLevel_ExperienceLevelId",
                table: "AspNetUsers",
                column: "ExperienceLevelId",
                principalTable: "ExperienceLevel",
                principalColumn: "ExperienceLevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
