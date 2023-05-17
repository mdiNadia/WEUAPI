using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeInEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ConfirmedResults_AdvertisingId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileSettings_Profiles_ProfileId",
                table: "ProfileSettings");

            migrationBuilder.DropIndex(
                name: "IX_ProfileSettings_ProfileId",
                table: "ProfileSettings");

            migrationBuilder.DropColumn(
                name: "ProfileSettingId",
                table: "Profiles");

            migrationBuilder.RenameColumn(
                name: "AdvertisingId",
                table: "Comments",
                newName: "ConfirmResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AdvertisingId",
                table: "Comments",
                newName: "IX_Comments_ConfirmResultId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Languages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CommentStatus",
                table: "ConfirmedResults",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ConfirmedResults_ConfirmResultId",
                table: "Comments",
                column: "ConfirmResultId",
                principalTable: "ConfirmedResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_ConfirmedResults_ConfirmResultId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "CommentStatus",
                table: "ConfirmedResults");

            migrationBuilder.RenameColumn(
                name: "ConfirmResultId",
                table: "Comments",
                newName: "AdvertisingId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ConfirmResultId",
                table: "Comments",
                newName: "IX_Comments_AdvertisingId");

            migrationBuilder.AddColumn<int>(
                name: "ProfileSettingId",
                table: "Profiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileSettings_ProfileId",
                table: "ProfileSettings",
                column: "ProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_ConfirmedResults_AdvertisingId",
                table: "Comments",
                column: "AdvertisingId",
                principalTable: "ConfirmedResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileSettings_Profiles_ProfileId",
                table: "ProfileSettings",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
