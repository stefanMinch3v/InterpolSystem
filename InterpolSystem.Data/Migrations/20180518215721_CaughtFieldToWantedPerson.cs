namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CaughtFieldToWantedPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCaught",
                table: "IdentityParticularsWanted",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCaught",
                table: "IdentityParticularsWanted");
        }
    }
}
