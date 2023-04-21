using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskCaseStatus",
                columns: table => new
                {
                    RiskCaseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCaseStatus", x => x.RiskCaseStatusId);
                });

            migrationBuilder.CreateTable(
                name: "RiskCaseType",
                columns: table => new
                {
                    RiskCaseTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCaseType", x => x.RiskCaseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "RiskCase",
                columns: table => new
                {
                    RiskCaseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskCaseTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RiskCaseStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCase", x => x.RiskCaseId);
                    table.ForeignKey(
                        name: "FK_RiskCase_RiskCaseStatus_RiskCaseStatusId",
                        column: x => x.RiskCaseStatusId,
                        principalTable: "RiskCaseStatus",
                        principalColumn: "RiskCaseStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiskCase_RiskCaseType_RiskCaseTypeId",
                        column: x => x.RiskCaseTypeId,
                        principalTable: "RiskCaseType",
                        principalColumn: "RiskCaseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskCase_RiskCaseStatusId",
                table: "RiskCase",
                column: "RiskCaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCase_RiskCaseTypeId",
                table: "RiskCase",
                column: "RiskCaseTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskCase");

            migrationBuilder.DropTable(
                name: "RiskCaseStatus");

            migrationBuilder.DropTable(
                name: "RiskCaseType");
        }
    }
}
