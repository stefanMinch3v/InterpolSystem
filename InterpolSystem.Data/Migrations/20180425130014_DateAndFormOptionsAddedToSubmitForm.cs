namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class DateAndFormOptionsAddedToSubmitForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SubmitForms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                table: "SubmitForms",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubmitForms");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                table: "SubmitForms");
        }
    }
}
