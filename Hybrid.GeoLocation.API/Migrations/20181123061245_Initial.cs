using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hybrid.GeoLocation.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    GeoNameId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocaleCode = table.Column<string>(nullable: true),
                    ContinentCode = table.Column<string>(nullable: true),
                    ContinentName = table.Column<string>(nullable: true),
                    CountryISOCode = table.Column<string>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    IsInEuropeanUnion = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.GeoNameId);
                    table.UniqueConstraint("AK_Countries_CountryISOCode", x => x.CountryISOCode);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    GeoNameId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocaleCode = table.Column<string>(nullable: true),
                    ContinentCode = table.Column<string>(nullable: true),
                    ContinentName = table.Column<string>(nullable: true),
                    CountryISOCode = table.Column<string>(nullable: false),
                    CountryName = table.Column<string>(nullable: true),
                    SubdivisionISOCode = table.Column<string>(nullable: true),
                    SubdivisionName = table.Column<string>(nullable: true),
                    Subdivision2ISOCode = table.Column<string>(nullable: true),
                    Subdivision2Name = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    MetroCode = table.Column<string>(nullable: true),
                    TimeZone = table.Column<string>(nullable: true),
                    IsInEuropeanUnion = table.Column<bool>(nullable: false),
                    CountryGeoNameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.GeoNameId);
                    table.ForeignKey(
                        name: "FK_Cities_Countries_CountryISOCode",
                        column: x => x.CountryISOCode,
                        principalTable: "Countries",
                        principalColumn: "CountryISOCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CityBlocks",
                columns: table => new
                {
                    GeoNameId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Network = table.Column<string>(nullable: true),
                    NetworkIPv6 = table.Column<string>(nullable: true),
                    RegisteredCountryGeoNameId = table.Column<int>(nullable: false),
                    RepresentedCountryGeoNameId = table.Column<int>(nullable: false),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    AccuracyRadius = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityBlocks", x => x.GeoNameId);
                    table.ForeignKey(
                        name: "FK_CityBlocks_Countries_RegisteredCountryGeoNameId",
                        column: x => x.RegisteredCountryGeoNameId,
                        principalTable: "Countries",
                        principalColumn: "GeoNameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryBlocks",
                columns: table => new
                {
                    GeoNameId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Network = table.Column<string>(nullable: true),
                    NetworkIPv6 = table.Column<string>(nullable: true),
                    RegisteredCountryGeoNameId = table.Column<int>(nullable: false),
                    RepresentedCountryGeoNameId = table.Column<int>(nullable: false),
                    IsAnonymousProxy = table.Column<bool>(nullable: false),
                    IsSatelliteProvider = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryBlocks", x => x.GeoNameId);
                    table.ForeignKey(
                        name: "FK_CountryBlocks_Countries_RegisteredCountryGeoNameId",
                        column: x => x.RegisteredCountryGeoNameId,
                        principalTable: "Countries",
                        principalColumn: "GeoNameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryISOCode",
                table: "Cities",
                column: "CountryISOCode");

            migrationBuilder.CreateIndex(
                name: "IX_CityBlocks_RegisteredCountryGeoNameId",
                table: "CityBlocks",
                column: "RegisteredCountryGeoNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryISOCode",
                table: "Countries",
                column: "CountryISOCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryBlocks_RegisteredCountryGeoNameId",
                table: "CountryBlocks",
                column: "RegisteredCountryGeoNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "CityBlocks");

            migrationBuilder.DropTable(
                name: "CountryBlocks");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
