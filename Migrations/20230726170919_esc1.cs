using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet_Portal.Migrations
{
    /// <inheritdoc />
    public partial class esc1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeModelEscalationMatrix");

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmployees",
                table: "Escalations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateOfJoin",
                table: "EmployeesModel",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleEmployees",
                table: "Escalations");

            migrationBuilder.AlterColumn<string>(
                name: "dateOfJoin",
                table: "EmployeesModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "EmployeeModelEscalationMatrix",
                columns: table => new
                {
                    EscalationMatrixId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleEmployeesemployeesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeModelEscalationMatrix", x => new { x.EscalationMatrixId, x.ResponsibleEmployeesemployeesID });
                    table.ForeignKey(
                        name: "FK_EmployeeModelEscalationMatrix_EmployeesModel_ResponsibleEmployeesemployeesID",
                        column: x => x.ResponsibleEmployeesemployeesID,
                        principalTable: "EmployeesModel",
                        principalColumn: "employeesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeModelEscalationMatrix_Escalations_EscalationMatrixId",
                        column: x => x.EscalationMatrixId,
                        principalTable: "Escalations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeModelEscalationMatrix_ResponsibleEmployeesemployeesID",
                table: "EmployeeModelEscalationMatrix",
                column: "ResponsibleEmployeesemployeesID");
        }
    }
}
