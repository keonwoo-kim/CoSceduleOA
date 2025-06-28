using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoScheduleOA.Migrations
{
    /// <inheritdoc />
    public partial class rating_index_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId",
                schema: "public",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_ItemId",
                schema: "public",
                table: "Ratings",
                columns: new[] { "UserId", "ItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ratings_UserId_ItemId",
                schema: "public",
                table: "Ratings");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                schema: "public",
                table: "Ratings",
                column: "UserId");
        }
    }
}
