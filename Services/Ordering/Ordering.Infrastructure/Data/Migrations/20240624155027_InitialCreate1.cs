using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Act_Ind",
                table: "Products",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Del_Ind",
                table: "Products",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Act_Ind",
                table: "Orders",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Del_Ind",
                table: "Orders",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Act_Ind",
                table: "OrderItems",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Del_Ind",
                table: "OrderItems",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Act_Ind",
                table: "Customers",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Del_Ind",
                table: "Customers",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Act_Ind",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Del_Ind",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Act_Ind",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Del_Ind",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Act_Ind",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Del_Ind",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Act_Ind",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Del_Ind",
                table: "Customers");
        }
    }
}
