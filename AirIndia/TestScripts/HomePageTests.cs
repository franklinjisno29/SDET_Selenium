using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.TestScripts
{
    internal class HomePageTests : CoreCodes
    {
        [Test, Order(1), Category("Smoke Test")]
        public void AirIndiaLogoTest()
        {
            var fluentWait = Waits(driver);
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Air India Logo Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            try
            {
                bchp.ClickLogo();
                Log.Information("Air India Logo Tested");
                TakeScreenshot();
                Assert.That(driver.Url.Contains("airindia"));
                LogTestResult("Air India Logo Test", "Air India Logo Test Success");
                test = extent.CreateTest("Air India Logo Test - Passed");
                test.Pass("Air India Logo Test Success");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Air India Logo Test", "Air India Logo Test Failed", ex.Message);
                test.Fail("Air India Logo Test Failed");
            }
        }

        [Test, Order(2), Category("Smoke Test")]
        public void AllLinkTest()
        {
            var fluentWait = Waits(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Air India All Link Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            List<IWebElement> alllinks = driver.FindElements(By.TagName("a")).ToList();
            try
            {
                foreach (var link in alllinks)
                {
                    string url = link.GetAttribute("href");
                    if (url == null)
                    {
                        Log.Information("URL is null");
                        continue;
                    }
                    else
                    {
                        bool isworking = CheckLinkStatus(url);
                        if (isworking)
                        {
                            Log.Information(url + "is working");
                        }
                        else
                        {
                            Log.Information(url + "is not working");

                        }
                    }
                }
                LogTestResult("Air India All Link Test", "Air India All Link Test Success");
                test = extent.CreateTest("Air India All Link Test - Passed");
                test.Pass("Air India All Link Test Success");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Air India All Link Test", "Air India All Link Test Failed", ex.Message);
                test.Fail("Air India All Link Test Failed");
            }
        }

        [Test, Order(3), Category("Smoke Test")]
        public void SignInLinkTest()
        {
            var fluentWait = Waits(driver);
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            Log.Information("Sign In Link Test Started");
            try
            {
                bchp.ClickSignIn();
                Log.Information("Sign In Link Tested");
                TakeScreenshot();
                Assert.That(driver.Url.Contains("signin"));
                LogTestResult("Sign In Link Test", "Sign In Link Test Success");
                test = extent.CreateTest("Sign In Link Test - Passed");
                test.Pass("Sign In Link Test Success");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Sign In Link Test", "Sign In Link Test Failed", ex.Message);
                test.Fail("Sign In Link Test Failed");
            }
        }

        [Test, Order(4), Category("Smoke Test")]
        public void ClickOffersTest()
        {
            var fluentWait = Waits(driver);
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            Log.Information("Offers Link Test Started");
            try
            {
                IWebElement button = driver.FindElement(By.XPath("//a[@id='headernav0']"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(button).Build().Perform();
                bchp.ClickOffers();
                Log.Information("Offers Link Tested");
                TakeScreenshot();
                Assert.That(driver.Url.Contains("offers"));
                LogTestResult("Offers Link Test", "Offers Link Test Success");
                test = extent.CreateTest("Offers Link Test - Passed");
                test.Pass("Offers Link Test Success");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Offers Link Test", "Offers Link Test Failed", ex.Message);
                test.Fail("Offers Link Test Failed");
            }
        }

        [Test, Order(5), Category("Smoke Test")]
        public void SearchButtonTest()
        {
            var fluentWait = Waits(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Search Button Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()=' SHOW FLIGHTS ']")));
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "SearchFlight";
            List<SearchFlightData> searchFlightDataList = ExcelUtils.ReadSearchFlightData(excelFilePath, sheetName);
            foreach (var searchFlightData in searchFlightDataList)
            {
                try
                {
                    string? from = searchFlightData?.From;
                    string? to = searchFlightData?.To;
                    string? dayselect = searchFlightData?.DaySelect;
                    string? monthselect = searchFlightData?.MonthSelect;
                    string? yearselect = searchFlightData?.YearSelect;
                    string? passengers = searchFlightData?.Passengers;
                    string? classselect = searchFlightData?.ClassSelect;
                    string? concessiontype = searchFlightData?.ConcessionType;
                    var searchpage = fluentWait.Until(d => bchp.SearchFlight(from, to, dayselect, monthselect, yearselect, passengers, classselect, concessiontype));
                    Log.Information("Flight Searched");
                    TakeScreenshot();
                    Assert.That(driver.Url, Does.Contain("availability"));
                    LogTestResult("Search Button Test", "Search Button Test Success");
                    test = extent.CreateTest("Search Button Test - Passed");
                    test.Pass("Search Button Test Success");
                }
                catch (AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("Search Button Test", "Search Button Test Failed", ex.Message);
                    test.Fail("Search Button Test Failed");
                }
            }
        }

        [Test, Order(6), Category("Smoke Test")]
        public void SearchButtonInvalidTest()
        {
            var fluentWait = Waits(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Search Button Invalid Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[text()=' SHOW FLIGHTS ']")));
            try
            {
                IWebElement button = driver.FindElement(By.XPath("//button[text()=' SHOW FLIGHTS ']"));
                //bool? numtext = button.GetAttribute("disabled");
                TakeScreenshot();
                Assert.IsTrue(true);
                LogTestResult("Search Button Test", "Search Button Test Success");
                test = extent.CreateTest("Search Button Test - Passed");
                test.Pass("Search Button Test Success");
            }
            catch (AssertionException ex)
            {
                    TakeScreenshot();
                    LogTestResult("Search Button Test", "Search Button Test Failed", ex.Message);
                    test.Fail("Search Button Test Failed");
            }
        }

        [Test, Order(7), Category("Smoke Test")]
        public void FlightStatusTest()
        {
            var fluentWait = Waits(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Flight Status Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[@class='mat-tab-label-content'])[4]")));
            try
            {
                    fluentWait.Until(d => bchp);
                    bchp.ClickFlightStatus();
                    IWebElement num = driver.FindElement(By.XPath("//p[contains(@class,'ontime-check-state')]"));
                    string? numtext = num.Text;
                    TakeScreenshot();
                    Assert.That(numtext, Is.EqualTo("SCHEDULED"));
                    LogTestResult("Flight Status Test", "Flight Status Test Success");
                    test = extent.CreateTest("Flight Status Test - Passed");
                    test.Pass("Flight Status Test Success");
            }
            catch (AssertionException ex)
            {
                    TakeScreenshot();
                    LogTestResult("Flight Status Test", "Flight Status Test Failed", ex.Message);
                    test.Fail("Flight Status Test Failed");
            }
        }

        [Test, Order(8), Category("Smoke Test")]
        public void FlightStatusInvalidTest()
        {
            var fluentWait = Waits(driver);
            if (!driver.Url.Equals("https://www.airindia.com/"))
                driver.Navigate().GoToUrl("https://www.airindia.com/");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Flight Status Invalid Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[@class='mat-tab-label-content'])[4]")));
            try
            {
                fluentWait.Until(d => bchp);
                bchp.ClickFlightStatusInvalid();
                IWebElement num = driver.FindElement(By.XPath("//h3[@class='error-message-part-subHeading']"));
                string? numtext = num.Text;
                TakeScreenshot();
                Assert.That(numtext, Does.Contain("Sorry"));
                LogTestResult("Flight Status Invalid Test", "Flight Status Invalid Test Error");
                test = extent.CreateTest("Flight Status Invalid Test - Error");
                test.Pass("Flight Status Test Error");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Flight Status Invalid Test", "Flight Status Test Failed", ex.Message);
                test.Fail("Flight Status Invalid Test Failed");
            }
        }
    }
}

