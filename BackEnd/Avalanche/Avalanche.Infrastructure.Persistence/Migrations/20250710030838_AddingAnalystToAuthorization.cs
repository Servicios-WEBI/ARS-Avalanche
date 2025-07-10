using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingAnalystToAuthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedAnalyst",
                table: "Authorizations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Analysts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations",
                column: "AssignedAnalyst",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authorizations_Analysts_AssignedAnalyst",
                table: "Authorizations",
                column: "AssignedAnalyst",
                principalTable: "Analysts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authorizations_Analysts_AssignedAnalyst",
                table: "Authorizations");

            migrationBuilder.DropTable(
                name: "Analysts");

            migrationBuilder.DropIndex(
                name: "IX_Authorizations_AssignedAnalyst",
                table: "Authorizations");

            migrationBuilder.DropColumn(
                name: "AssignedAnalyst",
                table: "Authorizations");
        }
    }
}
