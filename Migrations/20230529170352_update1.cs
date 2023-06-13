using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet_Portal.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesModel_EMatrix_EscalationMatrixId",
                table: "EmployeesModel");

            migrationBuilder.DropIndex(
                name: "IX_EmployeesModel_EscalationMatrixId",
                table: "EmployeesModel");

            migrationBuilder.DropColumn(
                name: "EscalationMatrixId",
                table: "EmployeesModel");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EMatrix",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EMatrix");

            migrationBuilder.AddColumn<int>(
                name: "EscalationMatrixId",
                table: "EmployeesModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesModel_EscalationMatrixId",
                table: "EmployeesModel",
                column: "EscalationMatrixId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesModel_EMatrix_EscalationMatrixId",
                table: "EmployeesModel",
                column: "EscalationMatrixId",
                principalTable: "EMatrix",
                principalColumn: "Id");
        }
    }
}
