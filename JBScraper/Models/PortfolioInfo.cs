using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JBScraper.Controllers;

namespace JBScraper.Models
{
    public class PortfolioInfo
    {
        [Key]
        public int PortfolioInfoId
        {
            get; set;
        }

        [Display(Name = "Capture Date")]
        [DataType(DataType.DateTime)]
        public DateTime CaptureDate
        {
            get; set;
        }

        [Display(Name = "Portfolio Value")]
        [DataType(DataType.Currency)]
        public double PortfolioValue
        {
            get; set;
        }

        [Display(Name = "Day Change")]
        [DataType(DataType.Currency)]
        public double DayGain
        {
            get; set;
        }

        [Display(Name = "Day Percent Change")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double PercentDayGain
        {
            get; set;
        }

        [Display(Name = "Total Change")]
        [DataType(DataType.Currency)]
        public double TotalGain
        {
            get; set;
        }

        [Display(Name = "Total Percent Change")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public double PercentTotalGain
        {
            get; set;
        }
        public virtual List<StockInfo> StockInfo { get; set; }
    }
}
