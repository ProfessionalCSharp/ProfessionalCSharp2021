using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksAPI.Migrations
{
    public partial class InitBook : Migration
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
                    Publisher = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    { 1, "sample pub", "title 1" },
                    { 73, "sample pub", "title 73" },
                    { 72, "sample pub", "title 72" },
                    { 71, "sample pub", "title 71" },
                    { 70, "sample pub", "title 70" },
                    { 69, "sample pub", "title 69" },
                    { 68, "sample pub", "title 68" },
                    { 67, "sample pub", "title 67" },
                    { 66, "sample pub", "title 66" },
                    { 65, "sample pub", "title 65" },
                    { 64, "sample pub", "title 64" },
                    { 63, "sample pub", "title 63" },
                    { 62, "sample pub", "title 62" },
                    { 61, "sample pub", "title 61" },
                    { 60, "sample pub", "title 60" },
                    { 59, "sample pub", "title 59" },
                    { 58, "sample pub", "title 58" },
                    { 57, "sample pub", "title 57" },
                    { 56, "sample pub", "title 56" },
                    { 55, "sample pub", "title 55" },
                    { 54, "sample pub", "title 54" },
                    { 53, "sample pub", "title 53" },
                    { 74, "sample pub", "title 74" },
                    { 52, "sample pub", "title 52" },
                    { 75, "sample pub", "title 75" },
                    { 77, "sample pub", "title 77" },
                    { 98, "sample pub", "title 98" },
                    { 97, "sample pub", "title 97" },
                    { 96, "sample pub", "title 96" },
                    { 95, "sample pub", "title 95" },
                    { 94, "sample pub", "title 94" },
                    { 93, "sample pub", "title 93" },
                    { 92, "sample pub", "title 92" },
                    { 91, "sample pub", "title 91" },
                    { 90, "sample pub", "title 90" },
                    { 89, "sample pub", "title 89" },
                    { 88, "sample pub", "title 88" },
                    { 87, "sample pub", "title 87" },
                    { 86, "sample pub", "title 86" },
                    { 85, "sample pub", "title 85" },
                    { 84, "sample pub", "title 84" },
                    { 83, "sample pub", "title 83" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 82, "sample pub", "title 82" },
                    { 81, "sample pub", "title 81" },
                    { 80, "sample pub", "title 80" },
                    { 79, "sample pub", "title 79" },
                    { 78, "sample pub", "title 78" },
                    { 76, "sample pub", "title 76" },
                    { 51, "sample pub", "title 51" },
                    { 50, "sample pub", "title 50" },
                    { 49, "sample pub", "title 49" },
                    { 22, "sample pub", "title 22" },
                    { 21, "sample pub", "title 21" },
                    { 20, "sample pub", "title 20" },
                    { 19, "sample pub", "title 19" },
                    { 18, "sample pub", "title 18" },
                    { 17, "sample pub", "title 17" },
                    { 16, "sample pub", "title 16" },
                    { 15, "sample pub", "title 15" },
                    { 14, "sample pub", "title 14" },
                    { 13, "sample pub", "title 13" },
                    { 12, "sample pub", "title 12" },
                    { 11, "sample pub", "title 11" },
                    { 10, "sample pub", "title 10" },
                    { 9, "sample pub", "title 9" },
                    { 8, "sample pub", "title 8" },
                    { 7, "sample pub", "title 7" },
                    { 6, "sample pub", "title 6" },
                    { 5, "sample pub", "title 5" },
                    { 4, "sample pub", "title 4" },
                    { 3, "sample pub", "title 3" },
                    { 2, "sample pub", "title 2" },
                    { 23, "sample pub", "title 23" },
                    { 24, "sample pub", "title 24" },
                    { 25, "sample pub", "title 25" },
                    { 26, "sample pub", "title 26" },
                    { 48, "sample pub", "title 48" },
                    { 47, "sample pub", "title 47" },
                    { 46, "sample pub", "title 46" },
                    { 45, "sample pub", "title 45" },
                    { 44, "sample pub", "title 44" },
                    { 43, "sample pub", "title 43" },
                    { 42, "sample pub", "title 42" },
                    { 41, "sample pub", "title 41" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Publisher", "Title" },
                values: new object[,]
                {
                    { 40, "sample pub", "title 40" },
                    { 39, "sample pub", "title 39" },
                    { 99, "sample pub", "title 99" },
                    { 38, "sample pub", "title 38" },
                    { 36, "sample pub", "title 36" },
                    { 35, "sample pub", "title 35" },
                    { 34, "sample pub", "title 34" },
                    { 33, "sample pub", "title 33" },
                    { 32, "sample pub", "title 32" },
                    { 31, "sample pub", "title 31" },
                    { 30, "sample pub", "title 30" },
                    { 29, "sample pub", "title 29" },
                    { 28, "sample pub", "title 28" },
                    { 27, "sample pub", "title 27" },
                    { 37, "sample pub", "title 37" },
                    { 100, "sample pub", "title 100" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
