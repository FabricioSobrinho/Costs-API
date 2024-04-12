using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostsApi.Migrations
{
    /// <inheritdoc />
    public partial class addNewNameField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Projects");
        }
    }
}
