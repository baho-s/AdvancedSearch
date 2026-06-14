using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedSearch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsEmbeddingDirtyToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmbeddingDirty",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmbeddingDirty",
                table: "Products");
        }
    }
}
