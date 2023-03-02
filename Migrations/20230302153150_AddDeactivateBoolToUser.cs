using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDeactivateBoolToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DeactivateRequest",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivateRequest",
                table: "AspNetUsers");
        }
    }
}
