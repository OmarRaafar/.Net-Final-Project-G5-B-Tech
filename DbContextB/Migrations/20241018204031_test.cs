using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContextB.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "46b1ad51-48a1-4374-85e8-c7dc67afd054");

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "9a902974-fec9-4fb9-b1f6-f3d2c979ded3", 0, "Hamza St", "Sohag", "24c78de3-811d-46fb-86f5-1d253ea21b29", "Egypt", "moh.alnoby216@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEOQX1X2vHlJpSEyYkWMx1LLAP6Jpoc3TElYXau8SdflvT158maBFF4qUB80UD9oLag==", null, false, "12345", "35af24f2-fe1d-45df-b1b0-ce4a7f11d7e5", false, "Mohammed Abbas", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Users",
                keyColumn: "Id",
                keyValue: "9a902974-fec9-4fb9-b1f6-f3d2c979ded3");

            migrationBuilder.InsertData(
                schema: "security",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Country", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { "46b1ad51-48a1-4374-85e8-c7dc67afd054", 0, "Hamza St", "Sohag", "1e3097e5-4049-4f81-bd15-570f2d255ed9", "Egypt", "moh.alnoby216@gmail.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEJSQS92AP7MwS6jo5OwUuEcw0rUM7EzzOB9Jk8MwXbFzkCWalDOfzUjy27f3OOvDbQ==", null, false, "12345", "a0a324fb-ace2-4171-b063-44d9f97dddb5", false, "Mohammed Abbas", "Admin" });
        }
    }
}
