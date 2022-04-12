using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Data.Migrations
{
    public partial class _20220411_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Browser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Browser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plataform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plataform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Url",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ShortUrl = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    OriginalUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UrlMetric",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateClicked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrowserId = table.Column<int>(type: "int", nullable: false),
                    PlataformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlMetric", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrlMetric_Browser_BrowserId",
                        column: x => x.BrowserId,
                        principalTable: "Browser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrlMetric_Plataform_PlataformId",
                        column: x => x.PlataformId,
                        principalTable: "Plataform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrlMetric_Url_UrlId",
                        column: x => x.UrlId,
                        principalTable: "Url",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Browser_Name",
                table: "Browser",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plataform_Name",
                table: "Plataform",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Url_OriginalUrl",
                table: "Url",
                column: "OriginalUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Url_ShortUrl",
                table: "Url",
                column: "ShortUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlMetric_BrowserId",
                table: "UrlMetric",
                column: "BrowserId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlMetric_PlataformId",
                table: "UrlMetric",
                column: "PlataformId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlMetric_UrlId",
                table: "UrlMetric",
                column: "UrlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlMetric");

            migrationBuilder.DropTable(
                name: "Browser");

            migrationBuilder.DropTable(
                name: "Plataform");

            migrationBuilder.DropTable(
                name: "Url");
        }
    }
}
