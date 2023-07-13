using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asset_amy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(191)", maxLength: 191, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(191)", maxLength: 191, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "enum('USER','ADMIN')", nullable: false, defaultValueSql: "'USER'", collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activationHash = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passwordResetHash = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createdAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    updatedAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "enum('P2P','STOCK','BOND','CRYPTO','REAL_ESTATE','COMMODITY','CASH')", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    updatedAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    belongsToId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "Asset_belongsToId_fkey",
                        column: x => x.belongsToId,
                        principalTable: "User",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    updatedAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    belongsToId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "Expense_belongsToId_fkey",
                        column: x => x.belongsToId,
                        principalTable: "User",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Revenue",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value = table.Column<double>(type: "double", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    updatedAt = table.Column<DateTime>(type: "datetime(3)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(3)"),
                    belongsToId = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "Revenue_belongsToId_fkey",
                        column: x => x.belongsToId,
                        principalTable: "User",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateIndex(
                name: "Asset_belongsToId_fkey",
                table: "Asset",
                column: "belongsToId");

            migrationBuilder.CreateIndex(
                name: "Expense_belongsToId_fkey",
                table: "Expense",
                column: "belongsToId");

            migrationBuilder.CreateIndex(
                name: "Revenue_belongsToId_fkey",
                table: "Revenue",
                column: "belongsToId");

            migrationBuilder.CreateIndex(
                name: "User_email_key",
                table: "User",
                column: "email",
                unique: true);

            SeedData(migrationBuilder);
        }

        private void SeedData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "firstName", "lastName", "email", "password", "createdAt" },
                values: new object[] { 1, "Robert", "Ackermann", "robert.ackermann@fh-erfurt.de", "$2b$10$lTarxieWGiXTEmEEqXiIsOB6IyVxZkEzf1NO/2M6iT6lbgG1nVouO", new DateTime() }
            );

            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "id", "name", "description", "value", "createdAt", "belongsToId" },
                values: new object[,] {
                    { 1, "Miete", "Lorem ipsum dolor sit amet.", 350, DateTime.Now, 1 },
                    { 2, "Konsumausgaben", "Lorem ipsum dolor sit amet.", 300, DateTime.Now, 1 },
                    { 3, "Krankenkasse", "Lorem ipsum dolor sit amet.", 122.78, DateTime.Now, 1 },
                    { 4, "Spotify", "Lorem ipsum dolor sit amet.", 9.99, DateTime.Now, 1 },
                    { 5, "McFit", "Lorem ipsum dolor sit amet.", 20, DateTime.Now, 1 },
                    { 6, "iCloud", "Lorem ipsum dolor sit amet.", 2.99, DateTime.Now, 1 },
                    { 7, "Reisekrankenversicherung", "Lorem ipsum dolor sit amet.", 0.75, DateTime.Now, 1 },
                    { 8, "Strom", "Lorem ipsum dolor sit amet.", 39, DateTime.Now, 1 },
                    { 9, "Mobilfunk", "Lorem ipsum dolor sit amet.", 10, DateTime.Now, 1 },
                    { 10, "Brillenversicherung", "Lorem ipsum dolor sit amet.", 10, DateTime.Now, 1 },
                    { 11, "BahnCard", "Lorem ipsum dolor sit amet.", 3.08, DateTime.Now, 1 },
                    { 12, "Hosting", "Lorem ipsum dolor sit amet.", 8, DateTime.Now, 1 },
                    { 13, "Internet", "Lorem ipsum dolor sit amet.", 34.99, DateTime.Now, 1 },
                    { 14, "Haftpflichtversicherung", "Lorem ipsum dolor sit amet.", 3.48, DateTime.Now, 1 },
                }
            );

            migrationBuilder.InsertData(
                table: "Revenue",
                columns: new[] { "id", "name", "description", "value", "createdAt", "belongsToId" },
                values: new object[,] {
                    { 1, "Bafög", "Lorem ipsum dolor sit amet.", 800, DateTime.Now, 1 },
                    { 2, "Aufzehrung von Ersparnissen", "Lorem ipsum dolor sit amet.", 200, DateTime.Now, 1 },
                }
            );

            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "id", "name", "description", "value", "type", "createdAt", "belongsToId" },
                values: new object[,] {
                    { 1, "Girokonto", "Lorem ipsum dolor sit amet.", 3000, "CASH", DateTime.Now, 1 },
                    { 2, "DKB Tagesgeldkonto", "Lorem ipsum dolor sit amet.", 5000, "CASH", DateTime.Now, 1 },
                    { 3, "ING-Depot", "Lorem ipsum dolor sit amet.", 10000, "STOCK", DateTime.Now, 1 },
                    { 4, "Bargeld", "Lorem ipsum dolor sit amet.", 250, "CASH", DateTime.Now, 1 },
                    { 5, "Kryptowährungen", "Lorem ipsum dolor sit amet.", 2500, "CRYPTO", DateTime.Now, 1 },
                    { 6, "Bondora Go & Grow", "Lorem ipsum dolor sit amet.", 2400, "P2P", DateTime.Now, 1 },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "Revenue");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
