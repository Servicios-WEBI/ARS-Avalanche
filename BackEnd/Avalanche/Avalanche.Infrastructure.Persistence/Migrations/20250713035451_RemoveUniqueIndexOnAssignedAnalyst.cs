using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexOnAssignedAnalyst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations",
                column: "AssignedAnalyst");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations",
                column: "AssignedAnalyst",
                unique: true);
        }
    }
}
