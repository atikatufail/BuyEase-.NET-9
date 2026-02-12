using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class FixProdPriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prodprice",
                table: "tbl_product");

            migrationBuilder.AddColumn<decimal>(
                name: "Prod_price",
                table: "tbl_product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_Cat_id",
                table: "tbl_product",
                column: "Cat_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_product_tbl_category_Cat_id",
                table: "tbl_product",
                column: "Cat_id",
                principalTable: "tbl_category",
                principalColumn: "Category_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_product_tbl_category_Cat_id",
                table: "tbl_product");

            migrationBuilder.DropIndex(
                name: "IX_tbl_product_Cat_id",
                table: "tbl_product");

            migrationBuilder.DropColumn(
                name: "Prod_price",
                table: "tbl_product");

            migrationBuilder.AddColumn<string>(
                name: "Prodprice",
                table: "tbl_product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
