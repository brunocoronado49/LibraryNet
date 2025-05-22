using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaApi.Migrations
{
    /// <inheritdoc />
    public partial class NuevasColumnasAutores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Autores",
                newName: "Names");

            migrationBuilder.AddColumn<string>(
                name: "Identification",
                table: "Autores",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNames",
                table: "Autores",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Identification",
                table: "Autores");

            migrationBuilder.DropColumn(
                name: "LastNames",
                table: "Autores");

            migrationBuilder.RenameColumn(
                name: "Names",
                table: "Autores",
                newName: "Name");
        }
    }
}
