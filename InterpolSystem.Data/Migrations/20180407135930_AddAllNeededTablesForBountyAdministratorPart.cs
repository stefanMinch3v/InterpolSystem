namespace InterpolSystem.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;

    public partial class AddAllNeededTablesForBountyAdministratorPart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countinents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countinents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EyeColor = table.Column<int>(nullable: false),
                    HairColor = table.Column<int>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    PictureUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    ScarsOrDistinguishingMarks = table.Column<string>(maxLength: 100, nullable: true),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    CountinentId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Countinents_CountinentId",
                        column: x => x.CountinentId,
                        principalTable: "Countinents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityParticularsMissing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllNames = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfDisappearance = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PhysicalDescriptionId = table.Column<int>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 50, nullable: false),
                    PlaceOfDisappearance = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityParticularsMissing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityParticularsMissing_PhysicalDescriptions_PhysicalDescriptionId",
                        column: x => x.PhysicalDescriptionId,
                        principalTable: "PhysicalDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityParticularsWanted",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllNames = table.Column<string>(maxLength: 100, nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    PhysicalDescriptionId = table.Column<int>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityParticularsWanted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityParticularsWanted_PhysicalDescriptions_PhysicalDescriptionId",
                        column: x => x.PhysicalDescriptionId,
                        principalTable: "PhysicalDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountriesNationalitiesMissing",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false),
                    IdentityParticularsMissingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesNationalitiesMissing", x => new { x.CountryId, x.IdentityParticularsMissingId });
                    table.ForeignKey(
                        name: "FK_CountriesNationalitiesMissing_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountriesNationalitiesMissing_IdentityParticularsMissing_IdentityParticularsMissingId",
                        column: x => x.IdentityParticularsMissingId,
                        principalTable: "IdentityParticularsMissing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguagesMissing",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false),
                    IdentityParticularsMissingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagesMissing", x => new { x.LanguageId, x.IdentityParticularsMissingId });
                    table.ForeignKey(
                        name: "FK_LanguagesMissing_IdentityParticularsMissing_IdentityParticularsMissingId",
                        column: x => x.IdentityParticularsMissingId,
                        principalTable: "IdentityParticularsMissing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagesMissing_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    IdentityParticularsWantedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_IdentityParticularsWanted_IdentityParticularsWantedId",
                        column: x => x.IdentityParticularsWantedId,
                        principalTable: "IdentityParticularsWanted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountriesNationalitiesWanted",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false),
                    IdentityParticularsWantedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountriesNationalitiesWanted", x => new { x.CountryId, x.IdentityParticularsWantedId });
                    table.ForeignKey(
                        name: "FK_CountriesNationalitiesWanted_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountriesNationalitiesWanted_IdentityParticularsWanted_IdentityParticularsWantedId",
                        column: x => x.IdentityParticularsWantedId,
                        principalTable: "IdentityParticularsWanted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguagesWanted",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false),
                    IdentityParticularsWantedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguagesWanted", x => new { x.LanguageId, x.IdentityParticularsWantedId });
                    table.ForeignKey(
                        name: "FK_LanguagesWanted_IdentityParticularsWanted_IdentityParticularsWantedId",
                        column: x => x.IdentityParticularsWantedId,
                        principalTable: "IdentityParticularsWanted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguagesWanted_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargesCountries",
                columns: table => new
                {
                    ChargesId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargesCountries", x => new { x.ChargesId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_ChargesCountries_Charges_ChargesId",
                        column: x => x.ChargesId,
                        principalTable: "Charges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChargesCountries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charges_IdentityParticularsWantedId",
                table: "Charges",
                column: "IdentityParticularsWantedId");

            migrationBuilder.CreateIndex(
                name: "IX_ChargesCountries_CountryId",
                table: "ChargesCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountinentId",
                table: "Countries",
                column: "CountinentId");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesNationalitiesMissing_IdentityParticularsMissingId",
                table: "CountriesNationalitiesMissing",
                column: "IdentityParticularsMissingId");

            migrationBuilder.CreateIndex(
                name: "IX_CountriesNationalitiesWanted_IdentityParticularsWantedId",
                table: "CountriesNationalitiesWanted",
                column: "IdentityParticularsWantedId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityParticularsMissing_PhysicalDescriptionId",
                table: "IdentityParticularsMissing",
                column: "PhysicalDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityParticularsWanted_PhysicalDescriptionId",
                table: "IdentityParticularsWanted",
                column: "PhysicalDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesMissing_IdentityParticularsMissingId",
                table: "LanguagesMissing",
                column: "IdentityParticularsMissingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguagesWanted_IdentityParticularsWantedId",
                table: "LanguagesWanted",
                column: "IdentityParticularsWantedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargesCountries");

            migrationBuilder.DropTable(
                name: "CountriesNationalitiesMissing");

            migrationBuilder.DropTable(
                name: "CountriesNationalitiesWanted");

            migrationBuilder.DropTable(
                name: "LanguagesMissing");

            migrationBuilder.DropTable(
                name: "LanguagesWanted");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "IdentityParticularsMissing");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "IdentityParticularsWanted");

            migrationBuilder.DropTable(
                name: "Countinents");

            migrationBuilder.DropTable(
                name: "PhysicalDescriptions");
        }
    }
}
