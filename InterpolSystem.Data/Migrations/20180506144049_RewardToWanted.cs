namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RewardToWanted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Reward",
                table: "IdentityParticularsWanted",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reward",
                table: "IdentityParticularsWanted");
        }
    }
}
