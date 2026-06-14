using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedSearch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductAddAllCommentProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllComment",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllComment",
                table: "Products");
        }
    }
}
