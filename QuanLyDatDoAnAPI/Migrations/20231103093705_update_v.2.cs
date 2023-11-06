using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyDatDoAnAPI.Migrations
{
    /// <inheritdoc />
    public partial class update_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AccountId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Order",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Account",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "AccountUserId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountUserId",
                table: "Order",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AccountUserId",
                table: "Order",
                column: "AccountUserId",
                principalTable: "Account",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Account_AccountUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_AccountUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Account",
                newName: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_AccountId",
                table: "Order",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Account_AccountId",
                table: "Order",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId");
        }
    }
}
