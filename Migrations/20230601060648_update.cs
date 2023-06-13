using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet_Portal.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Images",
                newName: "Imagesrc");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Imagesrc",
                table: "Images",
                newName: "ImageName");
        }
    }
}
