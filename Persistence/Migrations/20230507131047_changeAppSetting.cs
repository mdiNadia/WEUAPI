using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeAppSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerVisit",
                table: "Boosts");

            migrationBuilder.RenameColumn(
                name: "MinCostPerVisit",
                table: "AppSettings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "TransactionSign",
                table: "Transactions",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultPayRate",
                table: "Currencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ValuePerVisit",
                table: "Boosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinValuePerVisit",
                table: "AppSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionSign",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IsDefaultPayRate",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "ValuePerVisit",
                table: "Boosts");

            migrationBuilder.DropColumn(
                name: "MinValuePerVisit",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AppSettings",
                newName: "MinCostPerVisit");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerVisit",
                table: "Boosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
