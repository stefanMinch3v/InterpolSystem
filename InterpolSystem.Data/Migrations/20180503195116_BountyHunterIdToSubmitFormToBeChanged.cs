namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BountyHunterIdToSubmitFormToBeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SubmitForms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubmitForms_UserId",
                table: "SubmitForms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubmitForms_AspNetUsers_UserId",
                table: "SubmitForms",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubmitForms_AspNetUsers_UserId",
                table: "SubmitForms");

            migrationBuilder.DropIndex(
                name: "IX_SubmitForms_UserId",
                table: "SubmitForms");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SubmitForms");
        }
    }
}
