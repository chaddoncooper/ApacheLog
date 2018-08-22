using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apache.Log.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlacklistedResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FullPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlacklistedResources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WhitelistedResources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BasePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhitelistedResources", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlacklistedResources",
                columns: new[] { "Id", "FullPath" },
                values: new object[,]
                {
                    { 1, "/phpmyadmin/main.php" },
                    { 2, "/db/main.php" },
                    { 3, "/web/main.php" },
                    { 4, "/PMA/main.php" },
                    { 5, "/dbadmin/main.php" },
                    { 6, "/mysql/main.php" },
                    { 7, "/phpmyadmin2/main.php" },
                    { 8, "/phpmyadmin/read_dump.phpmain.php" },
                    { 9, "/PMA/read_dump.phpmain.php" },
                    { 10, "/mysql/read_dump.phpmain.php" },
                    { 11, "/xampp/phpmyadmin/read_dump.phpmain.php" },
                    { 12, "/typo3/phpmyadmin/read_dump.phpmain.php" },
                    { 13, "/mysqladmin/read_dump.phpmain.php" },
                    { 14, "/admin/read_dump.phpmain.php" },
                    { 15, "/db/read_dump.phpmain.php" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WhitelistedResources_BasePath",
                table: "WhitelistedResources",
                column: "BasePath",
                unique: true,
                filter: "[BasePath] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlacklistedResources");

            migrationBuilder.DropTable(
                name: "WhitelistedResources");
        }
    }
}
