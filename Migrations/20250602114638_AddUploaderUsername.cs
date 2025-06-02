using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShelf.Migrations
{
    /// <inheritdoc />
    public partial class AddUploaderUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "Books");
        }
    }
}
