using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CareerPhases",
                columns: table => new
                {
                    CareerPhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareerPhases", x => x.CareerPhaseId);
                });

            migrationBuilder.CreateTable(
                name: "CSInterests",
                columns: table => new
                {
                    CSInterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSInterests", x => x.CSInterestId);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceLevels",
                columns: table => new
                {
                    ExperienceLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceLevels", x => x.ExperienceLevelId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyId);
                });

            migrationBuilder.CreateTable(
                name: "NaturalLanguages",
                columns: table => new
                {
                    NaturalLanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalLanguages", x => x.NaturalLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguages",
                columns: table => new
                {
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguages", x => x.ProgrammingLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionString = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionId", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    SlackId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CareerPhaseId = table.Column<int>(type: "int", nullable: false),
                    ExperienceLevelId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_SlackId", x => x.SlackId);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CareerPhases_CareerPhaseId",
                        column: x => x.CareerPhaseId,
                        principalTable: "CareerPhases",
                        principalColumn: "CareerPhaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ExperienceLevels_ExperienceLevelId",
                        column: x => x.ExperienceLevelId,
                        principalTable: "ExperienceLevels",
                        principalColumn: "ExperienceLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CSInterestUsers",
                columns: table => new
                {
                    SlackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CSInterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSInterestUsers", x => new { x.SlackId, x.CSInterestId });
                    table.ForeignKey(
                        name: "FK_CSInterestUsers_AspNetUsers_SlackId",
                        column: x => x.SlackId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CSInterestUsers_CSInterests_CSInterestId",
                        column: x => x.CSInterestId,
                        principalTable: "CSInterests",
                        principalColumn: "CSInterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HobbyUsers",
                columns: table => new
                {
                    SlackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    HobbyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyUsers", x => new { x.SlackId, x.HobbyId });
                    table.ForeignKey(
                        name: "FK_HobbyUsers_AspNetUsers_SlackId",
                        column: x => x.SlackId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyUsers_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikerUser = table.Column<string>(name: "Liker User", type: "nvarchar(50)", nullable: false),
                    LikedUser = table.Column<string>(name: "Liked User", type: "nvarchar(50)", nullable: false),
                    IsMatch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.LikerUser, x.LikedUser });
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_Liked User",
                        column: x => x.LikedUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_Liker User",
                        column: x => x.LikerUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NaturalLanguageUsers",
                columns: table => new
                {
                    SlackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    NaturalLanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalLanguageUsers", x => new { x.SlackId, x.NaturalLanguageId });
                    table.ForeignKey(
                        name: "FK_NaturalLanguageUsers_AspNetUsers_SlackId",
                        column: x => x.SlackId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NaturalLanguageUsers_NaturalLanguages_NaturalLanguageId",
                        column: x => x.NaturalLanguageId,
                        principalTable: "NaturalLanguages",
                        principalColumn: "NaturalLanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammingLanguageUsers",
                columns: table => new
                {
                    SlackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProgrammingLanguageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammingLanguageUsers", x => new { x.SlackId, x.ProgrammingLanguageId });
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageUsers_AspNetUsers_SlackId",
                        column: x => x.SlackId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammingLanguageUsers_ProgrammingLanguages_ProgrammingLanguageId",
                        column: x => x.ProgrammingLanguageId,
                        principalTable: "ProgrammingLanguages",
                        principalColumn: "ProgrammingLanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswerBlocks",
                columns: table => new
                {
                    QuestionAnswerBlockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SlackId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    QuestionString = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AnswerString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("QuestionAnswerBlockId", x => x.QuestionAnswerBlockId);
                    table.ForeignKey(
                        name: "FK_QuestionAnswerBlocks_AspNetUsers_SlackId",
                        column: x => x.SlackId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rejections",
                columns: table => new
                {
                    RejectingUser = table.Column<string>(name: "Rejecting User", type: "nvarchar(50)", nullable: false),
                    RejectedUser = table.Column<string>(name: "Rejected User", type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rejections", x => new { x.RejectingUser, x.RejectedUser });
                    table.ForeignKey(
                        name: "FK_Rejections_AspNetUsers_Rejected User",
                        column: x => x.RejectedUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rejections_AspNetUsers_Rejecting User",
                        column: x => x.RejectingUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "SlackId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CSInterests",
                columns: new[] { "CSInterestId", "Name" },
                values: new object[,]
                {
                    { 1, "Artificial Intelligence" },
                    { 2, "Cybersecurity" },
                    { 3, "Database Technology" },
                    { 4, "Fintech" },
                    { 5, "Games Development" },
                    { 6, "Graphics Programming" },
                    { 7, "Intelligent Signal Processing" },
                    { 8, "Internet of Things" },
                    { 9, "Machine Learning" },
                    { 10, "Mobile Development" },
                    { 11, "Neural Networks" },
                    { 12, "Natural Language Processing" },
                    { 13, "Theoretical Computer Science" },
                    { 14, "Web Development" },
                    { 15, "User Experience" },
                    { 16, "Virtual Reality" }
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
                    { 2, "Adventure Sports" },
                    { 3, "Anime" },
                    { 4, "Archery" },
                    { 5, "Art" },
                    { 6, "Astrology" },
                    { 7, "Astronomy" },
                    { 8, "Baseball" },
                    { 9, "Basketball" },
                    { 10, "Biking" },
                    { 11, "Blogging" },
                    { 12, "Board Games" },
                    { 13, "Card Games" },
                    { 14, "Cars" },
                    { 15, "Chess" },
                    { 16, "Cinema" },
                    { 17, "Classical Music" },
                    { 18, "Comics" },
                    { 19, "Concerts" },
                    { 20, "Cooking" },
                    { 21, "Cosplaying" },
                    { 22, "Crafts" },
                    { 23, "Creative Writing" },
                    { 24, "Cycling" },
                    { 25, "Digital Art" },
                    { 26, "DIY" },
                    { 27, "DJing" },
                    { 28, "Drawing" },
                    { 29, "Economics" },
                    { 30, "Electronic Music" },
                    { 31, "Entrepreneurship" },
                    { 32, "Fashion" },
                    { 33, "Fiction" },
                    { 34, "Filmmaking" },
                    { 35, "Fitness" },
                    { 36, "Folk Music" },
                    { 37, "Football" },
                    { 38, "Golf" },
                    { 39, "Handball" },
                    { 40, "Hiking" },
                    { 41, "History" },
                    { 42, "Hockey" },
                    { 43, "Horseback Riding" },
                    { 44, "Jazz" },
                    { 45, "Learning Languages" },
                    { 46, "Mathematics" },
                    { 47, "Martial Arts" },
                    { 48, "Metal" },
                    { 49, "Music" },
                    { 50, "Musicals" },
                    { 51, "Music Production" },
                    { 52, "Non Fiction" },
                    { 53, "Opera" },
                    { 54, "Painting" },
                    { 55, "Pets" },
                    { 56, "Philosophy" },
                    { 57, "Photography" },
                    { 58, "Playing An Instrument" },
                    { 59, "Politics" },
                    { 60, "Pop Music" },
                    { 61, "Reading" },
                    { 62, "Restaurants" },
                    { 63, "Rock Music" },
                    { 64, "Rugby" },
                    { 65, "Running" },
                    { 66, "Sailing" },
                    { 67, "Sculpting" },
                    { 68, "Sewing" },
                    { 69, "Shopping" },
                    { 70, "Skiing" },
                    { 71, "Snow Sports" },
                    { 72, "Street Art" },
                    { 73, "Swimming" },
                    { 74, "Tennis" },
                    { 75, "Theatre" },
                    { 76, "Travel" },
                    { 77, "Video Games" },
                    { 78, "Volunteering" },
                    { 79, "Walking" },
                    { 80, "Water Sports" },
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
                    { 20, "Eastern Min" },
                    { 21, "English" },
                    { 22, "French" },
                    { 23, "Fula" },
                    { 24, "Gan Chinese" },
                    { 25, "German" },
                    { 26, "Greek" },
                    { 27, "Gujarati" },
                    { 28, "Haitian Creole" },
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
                    { 54, "Malay Indonesian" },
                    { 55, "Malayalam" },
                    { 56, "Mandarin" },
                    { 57, "Marathi" },
                    { 58, "Marwari" },
                    { 59, "Mossi" },
                    { 60, "Nepali" },
                    { 61, "Northern Min" },
                    { 62, "Odia Oriya" },
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
                    { 73, "Serbo Croatian" },
                    { 74, "Shona" },
                    { 75, "Sindhi" },
                    { 76, "Sinhalese" },
                    { 77, "Somali" },
                    { 78, "Southern Min" },
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
                    { 94, "Wu inc Shanghainese" },
                    { 95, "Xhosa" },
                    { 96, "Xiang Hunnanese" },
                    { 97, "Yoruba" },
                    { 98, "Yue Cantonese" },
                    { 99, "Zhuang" },
                    { 100, "Zulu" }
                });

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

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "QuestionString" },
                values: new object[,]
                {
                    { 1, "What is your dream job?" },
                    { 2, "What is your biggest fear?" },
                    { 3, "What did you want to be when you were small?" },
                    { 4, "If you could only eat one meal for the rest of your life, what would it be?" },
                    { 5, "If you were a super-hero, what powers would you have?" },
                    { 6, "If you could go back in time to change one thing, what would it be?" },
                    { 7, "What was the last book you read?" },
                    { 8, "What is your favorite childhood memory?" },
                    { 9, "Which of the five senses would you say is your strongest?" },
                    { 10, "What three things do you think of the most each day?" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CareerPhaseId",
                table: "AspNetUsers",
                column: "CareerPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExperienceLevelId",
                table: "AspNetUsers",
                column: "ExperienceLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SlackId",
                table: "AspNetUsers",
                column: "SlackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CSInterestUsers_CSInterestId",
                table: "CSInterestUsers",
                column: "CSInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_HobbyUsers_HobbyId",
                table: "HobbyUsers",
                column: "HobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_Liked User",
                table: "Likes",
                column: "Liked User");

            migrationBuilder.CreateIndex(
                name: "IX_NaturalLanguageUsers_NaturalLanguageId",
                table: "NaturalLanguageUsers",
                column: "NaturalLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammingLanguageUsers_ProgrammingLanguageId",
                table: "ProgrammingLanguageUsers",
                column: "ProgrammingLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswerBlocks_SlackId",
                table: "QuestionAnswerBlocks",
                column: "SlackId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionId",
                table: "Questions",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rejections_Rejected User",
                table: "Rejections",
                column: "Rejected User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CSInterestUsers");

            migrationBuilder.DropTable(
                name: "HobbyUsers");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "NaturalLanguageUsers");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguageUsers");

            migrationBuilder.DropTable(
                name: "QuestionAnswerBlocks");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Rejections");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CSInterests");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "NaturalLanguages");

            migrationBuilder.DropTable(
                name: "ProgrammingLanguages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CareerPhases");

            migrationBuilder.DropTable(
                name: "ExperienceLevels");

            migrationBuilder.DropTable(
                name: "Genders");
        }
    }
}
