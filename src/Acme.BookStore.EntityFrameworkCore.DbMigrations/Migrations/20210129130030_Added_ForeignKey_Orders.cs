using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class Added_ForeignKey_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppOrders_BookId",
                table: "AppOrders",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrders_AppBooks_BookId",
                table: "AppOrders",
                column: "BookId",
                principalTable: "AppBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrders_AppBooks_BookId",
                table: "AppOrders");

            migrationBuilder.DropIndex(
                name: "IX_AppOrders_BookId",
                table: "AppOrders");
        }
    }
}
