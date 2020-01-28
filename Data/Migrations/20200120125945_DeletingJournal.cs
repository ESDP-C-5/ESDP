using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM.Data.Migrations
{
    public partial class DeletingJournal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "Journals");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JournalId",
                table: "Attendances",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupID = table.Column<int>(nullable: false),
                    YearMonthAttendance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journals_Groups_GroupID",
                        column: x => x.GroupID,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journals_GroupID",
                table: "Journals",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Journals_JournalId",
                table: "Attendances",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
