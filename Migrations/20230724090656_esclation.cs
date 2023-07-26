using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet_Portal.Migrations
{
    /// <inheritdoc />
    public partial class esclation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Escalations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escalations", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeModelEscalationMatrix");

            migrationBuilder.DropTable(
                name: "Escalations");
        }
    }
}
