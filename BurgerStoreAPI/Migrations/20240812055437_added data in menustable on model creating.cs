using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BurgerStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class addeddatainmenustableonmodelcreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "ImageUrl", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, "/IMG/burger1.png", "Crispy Supreme", 100m, "veg" },
                    { 2, "/IMG/burger2.png", "Surprise", 100m, "veg" },
                    { 3, "/IMG/burger3.png", "Whopper", 100m, "veg" },
                    { 4, "/IMG/burger4.png", "Chilli Cheese", 100m, "veg" },
                    { 5, "/IMG/burger5.png", "Tandoor Gill", 100m, "veg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
