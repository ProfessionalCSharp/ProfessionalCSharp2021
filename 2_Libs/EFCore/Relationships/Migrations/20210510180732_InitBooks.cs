using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Relationships.Migrations
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
                    AddressLineOne = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressLineTwo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BusinessCountry = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    BusinessCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
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

            migrationBuilder.InsertData(
                schema: "bk",
                table: "Books",
                columns: new[] { "BookId", "Publisher", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(333), "Professional C#" },
                    { 11, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(13), "Professional C# 7 and .NET Core 2.0" },
                    { 10, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(17), "Professional C# 6 and .NET Core 1.0" },
                    { 9, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(71), "Professional C# 5.0 and .NET 4.5.1" },
                    { 8, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(6), "Professional C# 2012 and .NET 4.5" },
                    { 7, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(134), "Professional C# 4 and .NET 4" },
                    { 12, "Wrox Press", null, "Professional C# and .NET 2021 Edition" },
                    { 5, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(44), "Professional C# 2005 with .NET 3.0" },
                    { 4, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(45), "Professional C# 2005" },
                    { 3, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(83), "Professional C# 3rd Edition" },
                    { 2, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(35), "Professional C# 2nd Edition" },
                    { 6, "Wrox Press", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(23), "Professional C# 2008" }
                });

            migrationBuilder.InsertData(
                schema: "bk",
                table: "People",
                columns: new[] { "PersonId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 10, "Simon", "Robinson" },
                    { 1, "Allen", "Jones" },
                    { 2, "Bill", "Evjen" },
                    { 3, "Burton", "Harvey" },
                    { 4, "Christian", "Nagel" },
                    { 5, "Jay", "Glynn" },
                    { 6, "Karli", "Watson" },
                    { 7, "K S", "Allen" },
                    { 8, "Morgan", "Skinner" },
                    { 9, "Ollie", "Cornes" },
                    { 11, "Zach", "Greenvoss" }
                });

            migrationBuilder.InsertData(
                schema: "bk",
                table: "BookPerson",
                columns: new[] { "AuthorsPersonId", "WrittenBooksBookId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                schema: "bk",
                table: "BookPerson",
                columns: new[] { "AuthorsPersonId", "WrittenBooksBookId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                schema: "bk",
                table: "BookPerson",
                columns: new[] { "AuthorsPersonId", "WrittenBooksBookId" },
                values: new object[] { 2, 1 });

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
