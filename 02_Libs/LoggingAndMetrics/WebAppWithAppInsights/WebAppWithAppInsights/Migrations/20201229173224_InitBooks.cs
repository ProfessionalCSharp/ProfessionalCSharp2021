using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppWithAppInsights.Migrations
{
    public partial class InitBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "Sample Pub", "book 1" },
                    { 73, "Sample Pub", "book 73" },
                    { 72, "Sample Pub", "book 72" },
                    { 71, "Sample Pub", "book 71" },
                    { 70, "Sample Pub", "book 70" },
                    { 69, "Sample Pub", "book 69" },
                    { 68, "Sample Pub", "book 68" },
                    { 67, "Sample Pub", "book 67" },
                    { 66, "Sample Pub", "book 66" },
                    { 65, "Sample Pub", "book 65" },
                    { 64, "Sample Pub", "book 64" },
                    { 63, "Sample Pub", "book 63" },
                    { 62, "Sample Pub", "book 62" },
                    { 61, "Sample Pub", "book 61" },
                    { 60, "Sample Pub", "book 60" },
                    { 59, "Sample Pub", "book 59" },
                    { 58, "Sample Pub", "book 58" },
                    { 57, "Sample Pub", "book 57" },
                    { 56, "Sample Pub", "book 56" },
                    { 55, "Sample Pub", "book 55" },
                    { 54, "Sample Pub", "book 54" },
                    { 53, "Sample Pub", "book 53" },
                    { 74, "Sample Pub", "book 74" },
                    { 52, "Sample Pub", "book 52" },
                    { 75, "Sample Pub", "book 75" },
                    { 77, "Sample Pub", "book 77" },
                    { 98, "Sample Pub", "book 98" },
                    { 97, "Sample Pub", "book 97" },
                    { 96, "Sample Pub", "book 96" },
                    { 95, "Sample Pub", "book 95" },
                    { 94, "Sample Pub", "book 94" },
                    { 93, "Sample Pub", "book 93" },
                    { 92, "Sample Pub", "book 92" },
                    { 91, "Sample Pub", "book 91" },
                    { 90, "Sample Pub", "book 90" },
                    { 89, "Sample Pub", "book 89" },
                    { 88, "Sample Pub", "book 88" },
                    { 87, "Sample Pub", "book 87" },
                    { 86, "Sample Pub", "book 86" },
                    { 85, "Sample Pub", "book 85" },
                    { 84, "Sample Pub", "book 84" },
                    { 83, "Sample Pub", "book 83" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 82, "Sample Pub", "book 82" },
                    { 81, "Sample Pub", "book 81" },
                    { 80, "Sample Pub", "book 80" },
                    { 79, "Sample Pub", "book 79" },
                    { 78, "Sample Pub", "book 78" },
                    { 76, "Sample Pub", "book 76" },
                    { 51, "Sample Pub", "book 51" },
                    { 50, "Sample Pub", "book 50" },
                    { 49, "Sample Pub", "book 49" },
                    { 22, "Sample Pub", "book 22" },
                    { 21, "Sample Pub", "book 21" },
                    { 20, "Sample Pub", "book 20" },
                    { 19, "Sample Pub", "book 19" },
                    { 18, "Sample Pub", "book 18" },
                    { 17, "Sample Pub", "book 17" },
                    { 16, "Sample Pub", "book 16" },
                    { 15, "Sample Pub", "book 15" },
                    { 14, "Sample Pub", "book 14" },
                    { 13, "Sample Pub", "book 13" },
                    { 12, "Sample Pub", "book 12" },
                    { 11, "Sample Pub", "book 11" },
                    { 10, "Sample Pub", "book 10" },
                    { 9, "Sample Pub", "book 9" },
                    { 8, "Sample Pub", "book 8" },
                    { 7, "Sample Pub", "book 7" },
                    { 6, "Sample Pub", "book 6" },
                    { 5, "Sample Pub", "book 5" },
                    { 4, "Sample Pub", "book 4" },
                    { 3, "Sample Pub", "book 3" },
                    { 2, "Sample Pub", "book 2" },
                    { 23, "Sample Pub", "book 23" },
                    { 24, "Sample Pub", "book 24" },
                    { 25, "Sample Pub", "book 25" },
                    { 26, "Sample Pub", "book 26" },
                    { 48, "Sample Pub", "book 48" },
                    { 47, "Sample Pub", "book 47" },
                    { 46, "Sample Pub", "book 46" },
                    { 45, "Sample Pub", "book 45" },
                    { 44, "Sample Pub", "book 44" },
                    { 43, "Sample Pub", "book 43" },
                    { 42, "Sample Pub", "book 42" },
                    { 41, "Sample Pub", "book 41" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 40, "Sample Pub", "book 40" },
                    { 39, "Sample Pub", "book 39" },
                    { 99, "Sample Pub", "book 99" },
                    { 38, "Sample Pub", "book 38" },
                    { 36, "Sample Pub", "book 36" },
                    { 35, "Sample Pub", "book 35" },
                    { 34, "Sample Pub", "book 34" },
                    { 33, "Sample Pub", "book 33" },
                    { 32, "Sample Pub", "book 32" },
                    { 31, "Sample Pub", "book 31" },
                    { 30, "Sample Pub", "book 30" },
                    { 29, "Sample Pub", "book 29" },
                    { 28, "Sample Pub", "book 28" },
                    { 27, "Sample Pub", "book 27" },
                    { 37, "Sample Pub", "book 37" },
                    { 100, "Sample Pub", "book 100" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
