using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class ChangeTimeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTable_TimeTableId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeTable",
                table: "TimeTable");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "TimeTable");

            migrationBuilder.DropColumn(
                name: "DateStarT",
                table: "TimeTable");

            migrationBuilder.RenameTable(
                name: "TimeTable",
                newName: "TimeTables");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Levels",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Day1",
                table: "TimeTables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Day2",
                table: "TimeTables",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "TimeTables",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeTables",
                table: "TimeTables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTables_TimeTableId",
                table: "Groups",
                column: "TimeTableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_TimeTables_TimeTableId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeTables",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Day1",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Day2",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "TimeTables");

            migrationBuilder.RenameTable(
                name: "TimeTables",
                newName: "TimeTable");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Levels",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "TimeTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStarT",
                table: "TimeTable",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeTable",
                table: "TimeTable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_TimeTable_TimeTableId",
                table: "Groups",
                column: "TimeTableId",
                principalTable: "TimeTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
