using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProceduresWithEFCore.Migrations;

/// <inheritdoc />
public partial class AddGetBooksByYear : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var sp = @"
CREATE PROCEDURE [bk].[GetBooksByYear]
    @Year int
AS
BEGIN
    -- First result set: Books
    SELECT 
        b.BookId,
        b.Title,
        b.Publisher,
        b.ReleaseDate
    FROM bk.Books b
    WHERE YEAR(b.ReleaseDate) = @Year;

    -- Second result set: Authors with their book relationships
    SELECT 
        p.PersonId,
        p.FirstName,
        p.LastName,
        bp.WrittenBooksBookId as BookId
    FROM bk.People p
    INNER JOIN bk.BookPerson bp ON p.PersonId = bp.AuthorsPersonId
    INNER JOIN bk.Books b ON bp.WrittenBooksBookId = b.BookId
    WHERE YEAR(b.ReleaseDate) = @Year;
END";

        migrationBuilder.Sql(sp);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DROP PROCEDURE [bk].[GetBooksByYear]");
    }
}
