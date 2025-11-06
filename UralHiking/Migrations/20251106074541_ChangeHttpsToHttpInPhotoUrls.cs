using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UralHiking.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHttpsToHttpInPhotoUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE hiking_routes
                SET photo_url = REPLACE(photo_url, 'https://', 'http://')
                WHERE photo_url LIKE 'https://%';
                """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE hiking_routes
                SET photo_url = REPLACE(photo_url, 'http://', 'https://')
                WHERE photo_url LIKE 'http://%';
                """
            );
        }
    }
}
