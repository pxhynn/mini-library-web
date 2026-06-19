using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniLibrary.Web.Migrations
{
    /// <inheritdoc />
    public partial class ExpandSeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "AvailableCopies", "BookCode", "CategoryId", "RentalPrice", "Title" },
                values: new object[,]
                {
                    { 4, "Kenneth Rosen", 25, "MATH-003", 1, 18000m, "Discrete Mathematics" },
                    { 5, "Erich Gamma", 0, "CS-002", 2, 30000m, "Design Patterns" },
                    { 6, "Thomas Cormen", 3, "CS-003", 2, 35000m, "Introduction to Algorithms" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
