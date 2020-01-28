using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class EditedAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsAttended",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "Attendances",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Attendances");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAttended",
                table: "Attendances",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
