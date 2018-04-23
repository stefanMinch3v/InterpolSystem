namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class SubmitFormForWantedMissingPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubmitForms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentityParticularsMissingId = table.Column<int>(nullable: true),
                    IdentityParticularsWantedId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: false),
                    PersonImage = table.Column<byte[]>(maxLength: 2097152, nullable: true),
                    PoliceDepartment = table.Column<string>(nullable: true),
                    SenderEmail = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmitForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubmitForms_IdentityParticularsMissing_IdentityParticularsMissingId",
                        column: x => x.IdentityParticularsMissingId,
                        principalTable: "IdentityParticularsMissing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubmitForms_IdentityParticularsWanted_IdentityParticularsWantedId",
                        column: x => x.IdentityParticularsWantedId,
                        principalTable: "IdentityParticularsWanted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubmitForms_IdentityParticularsMissingId",
                table: "SubmitForms",
                column: "IdentityParticularsMissingId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmitForms_IdentityParticularsWantedId",
                table: "SubmitForms",
                column: "IdentityParticularsWantedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubmitForms");
        }
    }
}
