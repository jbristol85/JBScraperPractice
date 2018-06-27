using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JBScraper.Models
{
    public class StockInfo
    {
        [Display(Name = "Stock Symbol")]
        public string StockSymbol { get; set; }

        [Display(Name = "Current Price")]
        [DataType(DataType.Currency)]
        public double StockCurrentPrice { get; set; }

        [Display(Name = "Price Change")]
        [DataType(DataType.Currency)]
        public double StockPriceChange { get; set; }

        [Display(Name = "Price Change Percent")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double StockPriceChangePercent { get; set; }

        [Display(Name = "Shares")]
        public double StockShares { get; set; }

        [Display(Name = "Cost Basis")]
        [DataType(DataType.Currency)]
        public double StockCostBasis { get; set; }

        [Display(Name = "Market Value")]
        [DataType(DataType.Currency)]
        public double StockMarketValue { get; set; }

        [Display(Name = "Day Gain")]
        [DataType(DataType.Currency)]
        public double StockDayGain { get; set; }

        [Display(Name = "Day Gain Percent")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double StockDayGainPercent { get; set; }

        [Display(Name = "Total Gain")]
        [DataType(DataType.Currency)]
        public double StockTotalGain { get; set; }

        [Display(Name = "Total Gain Percent")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double StockTotalGainPercent { get; set; }

        public Int32 StockLots { get; set; }

        public string StockNotes { get; set; }

        
        public int StockInfoId { get; set; }

//        public int PortfolioInfoId { get; set; }
        public virtual PortfolioInfo PortfolioInfo{ get; set; }
    }
}
 