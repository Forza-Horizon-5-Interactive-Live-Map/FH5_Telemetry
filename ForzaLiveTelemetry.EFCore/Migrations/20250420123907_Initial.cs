using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForzaLiveTelemetry.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IPv4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 20, 14, 39, 6, 995, DateTimeKind.Local).AddTicks(3729))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
