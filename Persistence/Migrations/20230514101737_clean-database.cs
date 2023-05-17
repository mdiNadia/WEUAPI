using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cleandatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinHistories");

            migrationBuilder.DropTable(
                name: "TransferCoinHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RejectedResultAttachments",
                table: "RejectedResultAttachments");

            migrationBuilder.DropIndex(
                name: "IX_RejectedResultAttachments_RejectResultId",
                table: "RejectedResultAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikeComments",
                table: "LikeComments");

            migrationBuilder.DropIndex(
                name: "IX_LikeComments_ObserverId",
                table: "LikeComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_ObserverId",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfirmedResultAttachments",
                table: "ConfirmedResultAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmedResultAttachments_ConfirmResultId",
                table: "ConfirmedResultAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisingAttachments",
                table: "AdvertisingAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisingAttachments_AttachmentId",
                table: "AdvertisingAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdReports",
                table: "AdReports");

            migrationBuilder.DropIndex(
                name: "IX_AdReports_ObserverId",
                table: "AdReports");

            migrationBuilder.DropColumn(
                name: "Coin",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RejectedResultAttachments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LikeComments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ConfirmedResultAttachments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AdvertisingAttachments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AdReports");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UsersLoginHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProfileSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OrderRows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Connections",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RejectedResultAttachments",
                table: "RejectedResultAttachments",
                columns: new[] { "RejectResultId", "AttachmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikeComments",
                table: "LikeComments",
                columns: new[] { "ObserverId", "TargetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                columns: new[] { "ObserverId", "TargetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfirmedResultAttachments",
                table: "ConfirmedResultAttachments",
                columns: new[] { "ConfirmResultId", "AttachmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisingAttachments",
                table: "AdvertisingAttachments",
                columns: new[] { "AttachmentId", "AdvertisingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdReports",
                table: "AdReports",
                columns: new[] { "ObserverId", "TargetId" });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<long>(type: "bigint", nullable: false),
                    SaleReferenceId = table.Column<long>(type: "bigint", nullable: false),
                    StatusPayment = table.Column<bool>(type: "bit", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransferValueHistories",
                columns: table => new
                {
                    ObserverId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferValueHistories", x => new { x.ObserverId, x.TargetId });
                    table.ForeignKey(
                        name: "FK_TransferValueHistories_Profiles_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferValueHistories_Profiles_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferValueHistories_TargetId",
                table: "TransferValueHistories",
                column: "TargetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TransferValueHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RejectedResultAttachments",
                table: "RejectedResultAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LikeComments",
                table: "LikeComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfirmedResultAttachments",
                table: "ConfirmedResultAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisingAttachments",
                table: "AdvertisingAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdReports",
                table: "AdReports");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UsersLoginHistory");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProfileSettings");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OrderRows");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Connections");

            migrationBuilder.AddColumn<int>(
                name: "Coin",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RejectedResultAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LikeComments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ConfirmedResultAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AdvertisingAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AdReports",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RejectedResultAttachments",
                table: "RejectedResultAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikeComments",
                table: "LikeComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites",
                table: "Favorites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfirmedResultAttachments",
                table: "ConfirmedResultAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisingAttachments",
                table: "AdvertisingAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdReports",
                table: "AdReports",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CoinHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObserverId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinHistories_ConfirmedResults_TargetId",
                        column: x => x.TargetId,
                        principalTable: "ConfirmedResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoinHistories_Profiles_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferCoinHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ObserverId = table.Column<int>(type: "int", nullable: false),
                    TargetId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCoinHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferCoinHistories_Profiles_ObserverId",
                        column: x => x.ObserverId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCoinHistories_Profiles_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RejectedResultAttachments_RejectResultId",
                table: "RejectedResultAttachments",
                column: "RejectResultId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeComments_ObserverId",
                table: "LikeComments",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ObserverId",
                table: "Favorites",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedResultAttachments_ConfirmResultId",
                table: "ConfirmedResultAttachments",
                column: "ConfirmResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisingAttachments_AttachmentId",
                table: "AdvertisingAttachments",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdReports_ObserverId",
                table: "AdReports",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinHistories_ObserverId",
                table: "CoinHistories",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_CoinHistories_TargetId",
                table: "CoinHistories",
                column: "TargetId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCoinHistories_ObserverId",
                table: "TransferCoinHistories",
                column: "ObserverId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCoinHistories_TargetId",
                table: "TransferCoinHistories",
                column: "TargetId");
        }
    }
}
