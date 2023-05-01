using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminDetails",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminDetails", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeUsername = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId1 = table.Column<int>(type: "int", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskDetails_EmployeeDetails_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "EmployeeDetails",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_EmployeeId1",
                table: "TaskDetails",
                column: "EmployeeId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminDetails");

            migrationBuilder.DropTable(
                name: "TaskDetails");

            migrationBuilder.DropTable(
                name: "EmployeeDetails");
        }
    }
}
