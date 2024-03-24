using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookish.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAvailableCopies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: -1,
                column: "AvailableCopies",
                value: 4);

            migrationBuilder.UpdateData(
                table: "BooksOnLoan",
                keyColumn: "LoanId",
                keyValue: -1,
                columns: new[] { "DueDate", "IssueDate" },
                values: new object[] { new DateOnly(2024, 4, 23), new DateOnly(2024, 3, 24) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: -1,
                column: "AvailableCopies",
                value: 5);

            migrationBuilder.UpdateData(
                table: "BooksOnLoan",
                keyColumn: "LoanId",
                keyValue: -1,
                columns: new[] { "DueDate", "IssueDate" },
                values: new object[] { new DateOnly(2024, 4, 3), new DateOnly(2024, 3, 4) });
        }
    }
}
