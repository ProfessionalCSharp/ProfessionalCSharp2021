using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relationships.Migrations.Books
{
    public partial class InitBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bk");

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "bk",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "bk",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress_LineOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLineTwo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BusinessAddress_Location_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessAddress_Location_City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "BookPerson",
                schema: "bk",
                columns: table => new
                {
                    AuthorsPersonId = table.Column<int>(type: "int", nullable: false),
                    WrittenBooksBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPerson", x => new { x.AuthorsPersonId, x.WrittenBooksBookId });
                    table.ForeignKey(
                        name: "FK_BookPerson_Books_WrittenBooksBookId",
                        column: x => x.WrittenBooksBookId,
                        principalSchema: "bk",
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPerson_People_AuthorsPersonId",
                        column: x => x.AuthorsPersonId,
                        principalSchema: "bk",
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivateAddresses",
                schema: "bk",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    LineOne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineTwo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    City = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateAddresses", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PrivateAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "bk",
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPerson_WrittenBooksBookId",
                schema: "bk",
                table: "BookPerson",
                column: "WrittenBooksBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPerson",
                schema: "bk");

            migrationBuilder.DropTable(
                name: "PrivateAddresses",
                schema: "bk");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "bk");

            migrationBuilder.DropTable(
                name: "People",
                schema: "bk");
        }
    }
}
