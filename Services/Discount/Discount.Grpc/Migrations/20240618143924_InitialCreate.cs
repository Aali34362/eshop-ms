using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Crtd_User = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Lst_Crtd_User = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Crtd_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Lst_Crtd_Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Act_Ind = table.Column<short>(type: "INTEGER", nullable: false),
                    Del_Ind = table.Column<short>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
