using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewer.Migrations
{
    /// <inheritdoc />
    public partial class changeImageToImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Actor");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Director",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Actor",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Actor");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "User",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Movie",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Director",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Actor",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
