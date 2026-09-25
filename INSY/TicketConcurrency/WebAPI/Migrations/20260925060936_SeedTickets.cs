using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tickets = new object[100, 2];
            for (var id = 1; id <= 100; id++)
            {
                tickets[id - 1, 0] = id;
                tickets[id - 1, 1] = $"Ticket {id}";
            }

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Name" },
                values: tickets);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (var id = 1; id <= 100; id++)
            {
                migrationBuilder.DeleteData(
                    table: "Tickets",
                    keyColumn: "Id",
                    keyValue: id);
            }

        }
    }
}
