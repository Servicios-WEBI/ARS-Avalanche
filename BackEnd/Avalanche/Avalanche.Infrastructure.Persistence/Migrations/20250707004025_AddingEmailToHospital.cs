using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingEmailToHospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hospitals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hospitals");
        }
    }
}
