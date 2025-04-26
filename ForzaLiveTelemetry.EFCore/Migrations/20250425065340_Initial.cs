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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IPv4 = table.Column<string>(type: "text", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2025, 4, 25, 8, 53, 40, 299, DateTimeKind.Local).AddTicks(4164))
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
