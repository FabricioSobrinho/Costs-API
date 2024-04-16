using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CostsApi.Migrations
{
    /// <inheritdoc />
    public partial class addServiceConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceName",
                table: "Services",
                column: "ServiceName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceName",
                table: "Services");
        }
    }
}
