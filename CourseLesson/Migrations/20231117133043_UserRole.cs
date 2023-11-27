using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseLesson.Migrations
{
    /// <inheritdoc />
    public partial class UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "34f6f126-4e69-49a9-abf5-f9a7c6a94fc0", "f191eb6c-80db-45e9-b1c7-c1dfd5ab8a48", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34f6f126-4e69-49a9-abf5-f9a7c6a94fc0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a716d3d-1cc4-41ec-a332-0470bc2684fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9f8301ef-4007-4309-a9ae-1f8355c9eeb0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "964a8ccb-b8fd-4f76-89c5-0b2dcb57f0ff", "3f8c24ae-6a4c-4e4d-be81-b3c2ff1bcf74", "Seller", "SELLER" },
                    { "e570151e-1769-4653-990a-72f1ae20fbc1", "30acb34d-060b-412d-a57e-0e83f9adf850", "Underwriting", "UNDERWRITING" }
                });
        }
    }
}
