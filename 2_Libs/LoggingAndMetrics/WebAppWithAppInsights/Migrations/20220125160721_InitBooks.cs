using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                    { 2, "Sample Pub", "book 2" },
                    { 3, "Sample Pub", "book 3" },
                    { 4, "Sample Pub", "book 4" },
                    { 5, "Sample Pub", "book 5" },
                    { 6, "Sample Pub", "book 6" },
                    { 7, "Sample Pub", "book 7" },
                    { 8, "Sample Pub", "book 8" },
                    { 9, "Sample Pub", "book 9" },
                    { 10, "Sample Pub", "book 10" },
                    { 11, "Sample Pub", "book 11" },
                    { 12, "Sample Pub", "book 12" },
                    { 13, "Sample Pub", "book 13" },
                    { 14, "Sample Pub", "book 14" },
                    { 15, "Sample Pub", "book 15" },
                    { 16, "Sample Pub", "book 16" },
                    { 17, "Sample Pub", "book 17" },
                    { 18, "Sample Pub", "book 18" },
                    { 19, "Sample Pub", "book 19" },
                    { 20, "Sample Pub", "book 20" },
                    { 21, "Sample Pub", "book 21" },
                    { 22, "Sample Pub", "book 22" },
                    { 23, "Sample Pub", "book 23" },
                    { 24, "Sample Pub", "book 24" },
                    { 25, "Sample Pub", "book 25" },
                    { 26, "Sample Pub", "book 26" },
                    { 27, "Sample Pub", "book 27" },
                    { 28, "Sample Pub", "book 28" },
                    { 29, "Sample Pub", "book 29" },
                    { 30, "Sample Pub", "book 30" },
                    { 31, "Sample Pub", "book 31" },
                    { 32, "Sample Pub", "book 32" },
                    { 33, "Sample Pub", "book 33" },
                    { 34, "Sample Pub", "book 34" },
                    { 35, "Sample Pub", "book 35" },
                    { 36, "Sample Pub", "book 36" },
                    { 37, "Sample Pub", "book 37" },
                    { 38, "Sample Pub", "book 38" },
                    { 39, "Sample Pub", "book 39" },
                    { 40, "Sample Pub", "book 40" },
                    { 41, "Sample Pub", "book 41" },
                    { 42, "Sample Pub", "book 42" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 43, "Sample Pub", "book 43" },
                    { 44, "Sample Pub", "book 44" },
                    { 45, "Sample Pub", "book 45" },
                    { 46, "Sample Pub", "book 46" },
                    { 47, "Sample Pub", "book 47" },
                    { 48, "Sample Pub", "book 48" },
                    { 49, "Sample Pub", "book 49" },
                    { 50, "Sample Pub", "book 50" },
                    { 51, "Sample Pub", "book 51" },
                    { 52, "Sample Pub", "book 52" },
                    { 53, "Sample Pub", "book 53" },
                    { 54, "Sample Pub", "book 54" },
                    { 55, "Sample Pub", "book 55" },
                    { 56, "Sample Pub", "book 56" },
                    { 57, "Sample Pub", "book 57" },
                    { 58, "Sample Pub", "book 58" },
                    { 59, "Sample Pub", "book 59" },
                    { 60, "Sample Pub", "book 60" },
                    { 61, "Sample Pub", "book 61" },
                    { 62, "Sample Pub", "book 62" },
                    { 63, "Sample Pub", "book 63" },
                    { 64, "Sample Pub", "book 64" },
                    { 65, "Sample Pub", "book 65" },
                    { 66, "Sample Pub", "book 66" },
                    { 67, "Sample Pub", "book 67" },
                    { 68, "Sample Pub", "book 68" },
                    { 69, "Sample Pub", "book 69" },
                    { 70, "Sample Pub", "book 70" },
                    { 71, "Sample Pub", "book 71" },
                    { 72, "Sample Pub", "book 72" },
                    { 73, "Sample Pub", "book 73" },
                    { 74, "Sample Pub", "book 74" },
                    { 75, "Sample Pub", "book 75" },
                    { 76, "Sample Pub", "book 76" },
                    { 77, "Sample Pub", "book 77" },
                    { 78, "Sample Pub", "book 78" },
                    { 79, "Sample Pub", "book 79" },
                    { 80, "Sample Pub", "book 80" },
                    { 81, "Sample Pub", "book 81" },
                    { 82, "Sample Pub", "book 82" },
                    { 83, "Sample Pub", "book 83" },
                    { 84, "Sample Pub", "book 84" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 85, "Sample Pub", "book 85" },
                    { 86, "Sample Pub", "book 86" },
                    { 87, "Sample Pub", "book 87" },
                    { 88, "Sample Pub", "book 88" },
                    { 89, "Sample Pub", "book 89" },
                    { 90, "Sample Pub", "book 90" },
                    { 91, "Sample Pub", "book 91" },
                    { 92, "Sample Pub", "book 92" },
                    { 93, "Sample Pub", "book 93" },
                    { 94, "Sample Pub", "book 94" },
                    { 95, "Sample Pub", "book 95" },
                    { 96, "Sample Pub", "book 96" },
                    { 97, "Sample Pub", "book 97" },
                    { 98, "Sample Pub", "book 98" },
                    { 99, "Sample Pub", "book 99" },
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
