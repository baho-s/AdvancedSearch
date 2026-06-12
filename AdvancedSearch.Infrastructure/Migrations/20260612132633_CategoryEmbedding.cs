using Microsoft.EntityFrameworkCore.Migrations;
using Pgvector;

#nullable disable

namespace AdvancedSearch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryEmbedding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Vector>(
                name: "Embedding",
                table: "Categories",
                type: "vector(768)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Embedding",
                table: "Categories");
        }
    }
}
