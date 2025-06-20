using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Policies_Number",
                table: "Policies",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DocumentNumber",
                table: "Clients",
                column: "DocumentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Affiliates_DocumentNumber",
                table: "Affiliates",
                column: "DocumentNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Policies_Number",
                table: "Policies");

            migrationBuilder.DropIndex(
                name: "IX_Clients_DocumentNumber",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Affiliates_DocumentNumber",
                table: "Affiliates");
        }
    }
}
