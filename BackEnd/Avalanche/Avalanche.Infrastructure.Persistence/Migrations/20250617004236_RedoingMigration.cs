using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalanche.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RedoingMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorizationTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coverages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstitutionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MonthlyCost = table.Column<double>(type: "double precision", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanCoverages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AmountLimit = table.Column<double>(type: "double precision", nullable: false),
                    YearFrequencyLimit = table.Column<int>(type: "integer", nullable: false),
                    CoveragePercentage = table.Column<double>(type: "double precision", nullable: false),
                    PlanId = table.Column<string>(type: "text", nullable: false),
                    CoverageId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCoverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanCoverages_Coverages_CoverageId",
                        column: x => x.CoverageId,
                        principalTable: "Coverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanCoverages_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<string>(type: "text", nullable: false),
                    DocumentNumber = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    CustomerSince = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InstitutionTypeId = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hospitals_InstitutionTypes_InstitutionTypeId",
                        column: x => x.InstitutionTypeId,
                        principalTable: "InstitutionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hospitals_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Affiliates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<string>(type: "text", nullable: false),
                    DocumentNumber = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    AffiliateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Affiliates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Affiliates_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Affiliates_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    EffectiveStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<string>(type: "text", nullable: false),
                    PlanId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Policies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatePolicies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    AffiliationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DesAffiliationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    AffiliateId = table.Column<string>(type: "text", nullable: false),
                    PolicyId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatePolicies_Affiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalTable: "Affiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliatePolicies_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliatePolicies_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StatusId = table.Column<string>(type: "text", nullable: false),
                    AuthorizationTypeId = table.Column<string>(type: "text", nullable: false),
                    ApplicationAmount = table.Column<double>(type: "double precision", nullable: false),
                    ApprovedAmount = table.Column<double>(type: "double precision", nullable: true),
                    AffiliateId = table.Column<string>(type: "text", nullable: false),
                    PolicyId = table.Column<string>(type: "text", nullable: false),
                    HospitalId = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authorizations_Affiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalTable: "Affiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorizations_AuthorizationTypes_AuthorizationTypeId",
                        column: x => x.AuthorizationTypeId,
                        principalTable: "AuthorizationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorizations_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorizations_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Authorizations_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatePolicies_AffiliateId",
                table: "AffiliatePolicies",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatePolicies_PolicyId",
                table: "AffiliatePolicies",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatePolicies_StatusId",
                table: "AffiliatePolicies",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Affiliates_ClientId",
                table: "Affiliates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Affiliates_DocumentTypeId",
                table: "Affiliates",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Affiliates_StatusId",
                table: "Affiliates",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AffiliateId",
                table: "Authorizations",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_AuthorizationTypeId",
                table: "Authorizations",
                column: "AuthorizationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_HospitalId",
                table: "Authorizations",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_PolicyId",
                table: "Authorizations",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Authorizations_StatusId",
                table: "Authorizations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DocumentTypeId",
                table: "Clients",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StatusId",
                table: "Clients",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_InstitutionTypeId",
                table: "Hospitals",
                column: "InstitutionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_StatusId",
                table: "Hospitals",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanCoverages_CoverageId",
                table: "PlanCoverages",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanCoverages_PlanId",
                table: "PlanCoverages",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_ClientId",
                table: "Policies",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PlanId",
                table: "Policies",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_StatusId",
                table: "Policies",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliatePolicies");

            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropTable(
                name: "PlanCoverages");

            migrationBuilder.DropTable(
                name: "Affiliates");

            migrationBuilder.DropTable(
                name: "AuthorizationTypes");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Coverages");

            migrationBuilder.DropTable(
                name: "InstitutionTypes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Statuses");
        }
    }
}
