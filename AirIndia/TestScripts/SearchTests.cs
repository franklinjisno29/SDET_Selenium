using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.TestScripts
{
    internal class SearchTests : CoreCodes
    {
        [Test, Category("Regression Test")]
        public void SearchFlightTest()
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Search Flight Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
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
                    string? pId = searchFlightData?.PId;
                    string? firstName = searchFlightData?.FirstName;
                    string? lastName = searchFlightData?.LastName;
                    string? email = searchFlightData?.Email;
                    string? confirmEmail = searchFlightData?.ConfirmEmail;
                    string? countryCode = searchFlightData?.CountryCode;
                    string? mobileNo = searchFlightData?.MobileNo;
                    Console.WriteLine($"From: {from} - To: {to}, DepartDate: {dayselect}/{monthselect}/{yearselect}, Passengers: {passengers}, Class: {classselect}, Concession Type: {concessiontype}");
                    
                    var searchpage = fluentWait.Until(d => bchp.SearchFlight(from, to, dayselect, monthselect, yearselect, passengers, classselect, concessiontype));
                    Log.Information("Flight Searched");
                    var flightpage = fluentWait.Until(d => searchpage.ClickProduct(pId));
                    Log.Information("Selected Flight");
                    var travelerpage = fluentWait.Until(d => flightpage.ClickPassengerDetails());
                    Log.Information("Clicked Fill Passenger Details");
                    var cartpage = fluentWait.Until(d => travelerpage.FillPassengerDetails(firstName, lastName, email, confirmEmail, countryCode, mobileNo));
                    Log.Information("Passenger Details Filled");
                    Thread.Sleep(15000);
                    fluentWait.Until(d => cartpage);
                    cartpage.ClickCheckOutButton();
                    TakeScreenshot();
                    Assert.That(driver.Url, Does.Contain("payment"));
                    LogTestResult("Book a Flight", "Book a Flight Success");
                    test = extent.CreateTest("Book a Flight - Passed");
                    test.Pass("Book a Flight Success");
                }
                catch(AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("Book a Flight", "Book a Flight Failed", ex.Message);
                    test.Fail("Book a Flight Failed");
                }
            }
        }
    }
}
