using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JBScraper.Models;

namespace JBScraper.Controllers
{
    public class PortfolioInfoController : Controller
    {
        private readonly JBScraperContext _context;

        public PortfolioInfoController(JBScraperContext context)
        {
            _context = context;
        }

        // GET: PortfolioInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioInfo.ToListAsync());
        }

        // GET: PortfolioInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.PortfolioInfo
                .SingleOrDefaultAsync(m => m.PortfolioInfoId == id);
            var stocks = from s in _context.StockInfo.Where(m => m.PortfolioInfo.PortfolioInfoId == id)
                select s;

            if (portfolioInfo == null)
            {
                return NotFound();
            }

            portfolioInfo.StockInfo = await stocks.AsNoTracking().ToListAsync();
            return View(portfolioInfo);
        }

        // GET: PortfolioInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( PortfolioInfo portfolioInfo)
        {
            var captureScrape = GetCapture.Capture();
            if (ModelState.IsValid)
            {
                _context.Add(captureScrape);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine("error with scrape");
            }
            return View(captureScrape);
        }

        // GET: PortfolioInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.PortfolioInfo.SingleOrDefaultAsync(m => m.PortfolioInfoId == id);
            if (portfolioInfo == null)
            {
                return NotFound();
            }
            return View(portfolioInfo);
        }

        // POST: PortfolioInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PortfolioInfoId,CaptureDate,PortfolioValue,DayGain,PercentDayGain,TotalGain,PercentTotalGain")] PortfolioInfo portfolioInfo)
        {
            if (id != portfolioInfo.PortfolioInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioInfoExists(portfolioInfo.PortfolioInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioInfo);
        }

        // GET: PortfolioInfo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioInfo = await _context.PortfolioInfo
                .SingleOrDefaultAsync(m => m.PortfolioInfoId == id);
            if (portfolioInfo == null)
            {
                return NotFound();
            }

            return View(portfolioInfo);
        }

        // POST: PortfolioInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioInfo = await _context.PortfolioInfo.SingleOrDefaultAsync(m => m.PortfolioInfoId == id);
            _context.PortfolioInfo.Remove(portfolioInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioInfoExists(int id)
        {
            return _context.PortfolioInfo.Any(e => e.PortfolioInfoId == id);
        }
    }
}
