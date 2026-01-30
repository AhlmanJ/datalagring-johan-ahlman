using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Participants_Email_NotEmpty",
                table: "Participants");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Participants_Email_NotEmpty1",
                table: "Participants",
                sql: "LTRIM(RTRIM([Email])) <> ''");

            migrationBuilder.CreateIndex(
                name: "UQ_Participants_Email",
                table: "Instructors",
                column: "Email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Participants_Email_NotEmpty",
                table: "Instructors",
                sql: "LTRIM(RTRIM([Email])) <> ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Participants_Email_NotEmpty1",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "UQ_Participants_Email",
                table: "Instructors");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Participants_Email_NotEmpty",
                table: "Instructors");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Participants_Email_NotEmpty",
                table: "Participants",
                sql: "LTRIM(RTRIM([Email])) <> ''");
        }
    }
}
