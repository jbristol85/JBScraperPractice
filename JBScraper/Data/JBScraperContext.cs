using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JBScraper.Models
{
    public class JBScraperContext : DbContext
    {
        public JBScraperContext (DbContextOptions<JBScraperContext> options)
            : base(options)
        {
        }

        public DbSet<JBScraper.Models.PortfolioInfo> PortfolioInfo { get; set; }
    }
}
