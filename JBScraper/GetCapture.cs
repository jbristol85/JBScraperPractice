using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using JBScraper.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JBScraper
{
    public class GetCapture
    {
        public static PortfolioInfo Capture()
        {
            var capture = new PortfolioInfo();


//          

            var options = new ChromeOptions();
            options.AddArgument("--disable-gpu");
            // creates new chromeDriver and goes to webpage
            var chromeDriver = new ChromeDriver(@"C:\Users\joshb\source\repos\JBScraper\JBScraper\Services", options);
            chromeDriver.Navigate().GoToUrl("https://login.yahoo.com/");
            // waits for page to load
            //            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(14);
            // login sequence
            //            chromeDriver.FindElementById("uh-signedin").Click();
            chromeDriver.FindElementById("login-username").Click();
//            chromeDriver.Keyboard.SendKeys(username);
            chromeDriver.Keyboard.SendKeys(usernamePassword.YahooUsername());
            chromeDriver.Keyboard.SendKeys(Keys.Enter);
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            chromeDriver.FindElementById("login-passwd").Click();
//            chromeDriver.Keyboard.SendKeys(password1);
            chromeDriver.Keyboard.SendKeys(usernamePassword.YahooPassword());
            chromeDriver.Keyboard.SendKeys(Keys.Enter);
            //navigate to portfolio with stocks
            //            chromeDriver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios?bypass=true");

            //            chromeDriver.FindElementById("__dialog").Click();
            //            chromeDriver.Keyboard.SendKeys(Keys.Escape);
            //navigate to Jbristol Portfolio
            chromeDriver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_1/view/v2");
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            chromeDriver.FindElement(By.Id("__dialog")).Click();
            chromeDriver.Keyboard.SendKeys(Keys.Escape);
            // get currentValue dayGains and totalGain
            var currentValue = chromeDriver
                .FindElementByXPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[1]").Text;
            var dayGain = chromeDriver
                .FindElementByXPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[2]/span").Text.Split(' ');
            var totalGain = chromeDriver
                .FindElementByXPath("//*[@id=\"main\"]/section/header/div/div[1]/div/div[2]/p[3]/span").Text.Split(' ');
            var dayGainPercent = dayGain[1];
            var totalGainPercent = totalGain[1];
            var PortfolioValue = double.Parse(currentValue, NumberStyles.Currency);
            var DayGain = double.Parse(dayGain[0]);
            //            capture.PercentDayGain = double.Parse(dayGainPercent.Remove(0, 1).Remove(dayGainPercent.Length - 2, dayGainPercent.Length - 1));
            var PercentDayGain = (double.Parse(dayGainPercent.Trim(new char[] { ' ', '(', '%', ')' }))/100);
            var TotalGain = double.Parse(totalGain[0]);
            //            capture.PercentTotalGain = double.Parse(totalGainPercent.Remove(0, 1).Remove(dayGainPercent.Length - 2, dayGainPercent.Length - 1));
            var PercentTotalGain = (double.Parse(totalGainPercent.Trim(new char[] { ' ', '(', '%', ')' }))/100);
            Console.WriteLine(
                "currentValue: {0}, dayGain: {1}, totalGain: {2}, dayGainPercent: {3}, totalGainPercent: {4}",
                PortfolioValue, DayGain, TotalGain, PercentDayGain, PercentTotalGain);

            //            // scrape symbols
            var portfolioStockInfo = new List<StockInfo>();
            var table = chromeDriver.FindElement(By.TagName("table"));
//            Console.WriteLine("Table" + table);
            var tableRows = table.FindElements(By.TagName("tr"));
            Console.WriteLine("tablerows" + tableRows);
            var listRowData = new List<string>();

            // each row in the table
            foreach (var row in tableRows)
            {
                var tableColumn = row.FindElements(By.TagName("td"));
                if (tableColumn.Count > 0)
                {

                    foreach (var col in tableRows)
                    {
                        listRowData.Add(col.Text);
                    }

                    var symbolPrice = listRowData[0].Split('\n');
                    var changePercentDollar = listRowData[1].Split('\n');
                    var dayGainPercentDollar = listRowData[5].Split('\n');
                    var totalGainPercentDollar = listRowData[6].Split('\n');
                    var numLots = listRowData[7].Split(' ');

                    portfolioStockInfo.Add(new StockInfo()
                    {

                        StockSymbol = symbolPrice[0],
                        StockCurrentPrice = Double.Parse(symbolPrice[1]),
                        StockPriceChange = Double.Parse(changePercentDollar[1].Trim(new char[] {' ', '+'})),
                        StockPriceChangePercent =
                            (Double.Parse(changePercentDollar[0].Trim(new char[] {' ', '+', '%'})) / 100),
                        StockShares = Double.Parse(listRowData[2]),
                        StockCostBasis = Double.Parse(listRowData[3]),
                        StockMarketValue = Double.Parse(listRowData[4]),
                        StockDayGain = Double.Parse(dayGainPercentDollar[1]),
                        StockDayGainPercent =
                            (Double.Parse(dayGainPercentDollar[0].Trim(new char[] {' ', '+', '%'})) / 100),
                        StockTotalGain = Double.Parse(totalGainPercentDollar[1]),
                        StockTotalGainPercent =
                            (Double.Parse(totalGainPercentDollar[0].Trim(new char[] {' ', '+', '%'})) / 100),
                        StockLots = Int32.Parse(numLots[0]),
                        StockNotes = listRowData[8]
                    });
                    Console.WriteLine("going to clear");
                    listRowData.Clear();
                }

            

            
            }
            chromeDriver.Quit();
            Console.WriteLine("portfolioStockInfo:   " + portfolioStockInfo);
            capture.StockInfo = portfolioStockInfo;
            return capture;
        }
    }
}
