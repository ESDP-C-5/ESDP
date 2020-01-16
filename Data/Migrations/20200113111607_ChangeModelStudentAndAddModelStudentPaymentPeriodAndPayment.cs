using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;


namespace CRM.Data.Migrations
{
    public partial class ChangeModelStudentAndAddModelStudentPaymentPeriodAndPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEndStudying",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataStartStudying",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StudentPaymentAndPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    MustTotal = table.Column<decimal>(nullable: false),
                    PaymentPeriodStart = table.Column<DateTime>(nullable: false),
                    PaymentPeriodEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPaymentAndPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPaymentAndPeriods_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    StudentPaymentAndPeriodId = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    DateTimePayment = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_StudentPaymentAndPeriods_StudentPaymentAndPeriodId",
                        column: x => x.StudentPaymentAndPeriodId,
                        principalTable: "StudentPaymentAndPeriods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentId",
                table: "Payments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentPaymentAndPeriodId",
                table: "Payments",
                column: "StudentPaymentAndPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPaymentAndPeriods_StudentId",
                table: "StudentPaymentAndPeriods",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StudentPaymentAndPeriods");

            migrationBuilder.DropColumn(
                name: "DataEndStudying",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DataStartStudying",
                table: "Students");
        }
    }
}
