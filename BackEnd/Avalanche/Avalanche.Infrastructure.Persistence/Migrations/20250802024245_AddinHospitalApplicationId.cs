using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddinHospitalApplicationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalApplicationId",
                table: "Authorizations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_HospitalApplicationId",
                table: "Authorizations",
                column: "HospitalApplicationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authorizations_HospitalApplicationId",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "HospitalApplicationId",
                table: "Authorizations");
        }
    }
}
