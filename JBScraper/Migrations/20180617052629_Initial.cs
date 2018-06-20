using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JBScraper.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PortfolioInfo",
                columns: table => new
                {
                    PortfolioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CaptureDate = table.Column<DateTime>(nullable: false),
                    PortfolioValue = table.Column<double>(nullable: false),
                    DayGain = table.Column<double>(nullable: false),
                    PercentDayGain = table.Column<double>(nullable: false),
                    TotalGain = table.Column<double>(nullable: false),
                    PercentTotalGain = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioInfo", x => x.PortfolioID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioInfo");
        }
    }
}
