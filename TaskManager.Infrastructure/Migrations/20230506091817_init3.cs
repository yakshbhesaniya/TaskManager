using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedTaskTime",
                table: "TaskDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskTotalTime",
                table: "TaskDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ModuleDetails",
                columns: table => new
                {
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TasksTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModuleDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModuleTotalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModuleStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleDetails", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_ModuleDetails_TaskDetails_TasksTaskId",
                        column: x => x.TasksTaskId,
                        principalTable: "TaskDetails",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDetails_TasksTaskId",
                table: "ModuleDetails",
                column: "TasksTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleDetails");

            migrationBuilder.DropColumn(
                name: "AssignedTaskTime",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "TaskTotalTime",
                table: "TaskDetails");
        }
    }
}
