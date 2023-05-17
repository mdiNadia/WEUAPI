using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeCoinHistoryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferCoinHistories",
                table: "TransferCoinHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinHistories",
                table: "CoinHistories");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TransferCoinHistories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoinHistories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferCoinHistories",
                table: "TransferCoinHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinHistories",
                table: "CoinHistories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCoinHistories_ObserverId",
                table: "TransferCoinHistories",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinHistories_ObserverId",
                table: "CoinHistories",
                column: "ObserverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferCoinHistories",
                table: "TransferCoinHistories");

            migrationBuilder.DropIndex(
                name: "IX_TransferCoinHistories_ObserverId",
                table: "TransferCoinHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinHistories",
                table: "CoinHistories");

            migrationBuilder.DropIndex(
                name: "IX_CoinHistories_ObserverId",
                table: "CoinHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TransferCoinHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoinHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferCoinHistories",
                table: "TransferCoinHistories",
                columns: new[] { "ObserverId", "TargetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinHistories",
                table: "CoinHistories",
                columns: new[] { "ObserverId", "TargetId" });
        }
    }
}
