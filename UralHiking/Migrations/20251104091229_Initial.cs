using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UralHiking.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "gear_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(type: "text", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gear_items", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hiking_routes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    short_description = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    distance_meters = table.Column<int>(type: "integer", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false),
                    difficulty_internal = table.Column<int>(type: "integer", nullable: false),
                    ascent_meters = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    review_count = table.Column<int>(type: "integer", nullable: false),
                    photo_url = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_hiking_routes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coordinates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    hiking_route_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coordinates", x => x.id);
                    table.ForeignKey(
                        name: "fk_coordinates_hiking_routes_hiking_route_id",
                        column: x => x.hiking_route_id,
                        principalTable: "hiking_routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gear_item_hiking_route",
                columns: table => new
                {
                    gear_items_id = table.Column<int>(type: "integer", nullable: false),
                    hiking_routes_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_gear_item_hiking_route", x => new { x.gear_items_id, x.hiking_routes_id });
                    table.ForeignKey(
                        name: "fk_gear_item_hiking_route_gear_items_gear_items_id",
                        column: x => x.gear_items_id,
                        principalTable: "gear_items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_gear_item_hiking_route_hiking_routes_hiking_routes_id",
                        column: x => x.hiking_routes_id,
                        principalTable: "hiking_routes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_coordinates_hiking_route_id",
                table: "coordinates",
                column: "hiking_route_id");

            migrationBuilder.CreateIndex(
                name: "ix_gear_item_hiking_route_hiking_routes_id",
                table: "gear_item_hiking_route",
                column: "hiking_routes_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "coordinates");

            migrationBuilder.DropTable(
                name: "gear_item_hiking_route");

            migrationBuilder.DropTable(
                name: "gear_items");

            migrationBuilder.DropTable(
                name: "hiking_routes");
        }
    }
}
