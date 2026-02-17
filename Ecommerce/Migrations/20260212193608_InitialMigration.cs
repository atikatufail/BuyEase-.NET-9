using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_admin",
                columns: table => new
                {
                    Admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Admin_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin_image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admin", x => x.Admin_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_category",
                columns: table => new
                {
                    Category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.Category_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_customer",
                columns: table => new
                {
                    Cust_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cust_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cust_image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_customer", x => x.Cust_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_faq",
                columns: table => new
                {
                    Faq_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fa_ques = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faq_ans = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_faq", x => x.Faq_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_feedback",
                columns: table => new
                {
                    Feed_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_msg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedback", x => x.Feed_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_product",
                columns: table => new
                {
                    Prod_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prod_price = table.Column<int>(type: "int", nullable: false),
                    Prod_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prod_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cat_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_product", x => x.Prod_id);
                    table.ForeignKey(
                        name: "FK_tbl_product_tbl_category_Cat_id",
                        column: x => x.Cat_id,
                        principalTable: "tbl_category",
                        principalColumn: "Category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_cart",
                columns: table => new
                {
                    Cart_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prod_id = table.Column<int>(type: "int", nullable: false),
                    Prod_quantity = table.Column<int>(type: "int", nullable: false),
                    Cust_id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_cart", x => x.Cart_id);
                    table.ForeignKey(
                        name: "FK_tbl_cart_tbl_customer_Cust_id",
                        column: x => x.Cust_id,
                        principalTable: "tbl_customer",
                        principalColumn: "Cust_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_cart_tbl_product_Prod_id",
                        column: x => x.Prod_id,
                        principalTable: "tbl_product",
                        principalColumn: "Prod_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_Cust_id",
                table: "tbl_cart",
                column: "Cust_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_cart_Prod_id",
                table: "tbl_cart",
                column: "Prod_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_product_Cat_id",
                table: "tbl_product",
                column: "Cat_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_admin");

            migrationBuilder.DropTable(
                name: "tbl_cart");

            migrationBuilder.DropTable(
                name: "tbl_faq");

            migrationBuilder.DropTable(
                name: "tbl_feedback");

            migrationBuilder.DropTable(
                name: "tbl_customer");

            migrationBuilder.DropTable(
                name: "tbl_product");

            migrationBuilder.DropTable(
                name: "tbl_category");
        }
    }
}
