using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Back_end.Migrations
{
    /// <inheritdoc />
    public partial class UserHasOnlyOneHome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Homes_UserId",
                table: "Homes");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_UserId",
                table: "Homes",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Homes_UserId",
                table: "Homes");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_UserId",
                table: "Homes",
                column: "UserId");
        }
    }
}
