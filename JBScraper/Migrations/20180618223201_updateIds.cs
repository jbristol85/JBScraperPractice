using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JBScraper.Migrations
{
    public partial class updateIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockInfo",
                columns: table => new
                {
                    StockSymbol = table.Column<string>(nullable: true),
                    StockCurrentPrice = table.Column<double>(nullable: false),
                    StockPriceChange = table.Column<double>(nullable: false),
                    StockPriceChangePercent = table.Column<double>(nullable: false),
                    StockShares = table.Column<double>(nullable: false),
                    StockCostBasis = table.Column<double>(nullable: false),
                    StockMarketValue = table.Column<double>(nullable: false),
                    StockDayGain = table.Column<double>(nullable: false),
                    StockDayGainPercent = table.Column<double>(nullable: false),
                    StockTotalGain = table.Column<double>(nullable: false),
                    StockTotalGainPercent = table.Column<double>(nullable: false),
                    StockLots = table.Column<int>(nullable: false),
                    StockNotes = table.Column<string>(nullable: true),
                    StockInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PortfolioInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfo", x => x.StockInfoId);
                    table.ForeignKey(
                        name: "FK_StockInfo_PortfolioInfo_PortfolioInfoId",
                        column: x => x.PortfolioInfoId,
                        principalTable: "PortfolioInfo",
                        principalColumn: "PortfolioInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_PortfolioInfoId",
                table: "StockInfo",
                column: "PortfolioInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockInfo");
        }
    }
}
