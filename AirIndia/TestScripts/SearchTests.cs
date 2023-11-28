using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        [Test]
        public void SearchFlightTest()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(50);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "element not found";
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Search Flight Test Started");
            Thread.Sleep(2000);

            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "SearchFlight";

            List<SearchFlightData> searchFlightDataList = ExcelUtils.ReadSearchFlightData(excelFilePath, sheetName);

            foreach (var searchFlightData in searchFlightDataList)
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
                
                var flightpage = fluentWait.Until(d => searchpage.ClickProduct(pId));
                
                var travelerpage = fluentWait.Until(d => flightpage.ClickPassengerDetails());
                
                var cartpage = fluentWait.Until(d => travelerpage.FillPassengerDetails(firstName,lastName,email,confirmEmail,countryCode,mobileNo));

                var paymentpage = fluentWait.Until(d => cartpage.ClickCheckOutButton());

                paymentpage.CheckOutProcess();

                
                //    try
                //{
                //    Assert.IsTrue(driver?.FindElement(By.XPath("//div[" +
                //       "@class='modal-inner-wrap']//following::h1[2]")).Text == "Create an Account", $"Test failed for Create Account");
                //    LogTestResult("Create Account Link Test", "Create Account Link Success");
                //}
                //catch (AssertionException ex)
                //{
                //    LogTestResult("Create Account Link Test", "Create Account Link Failed", ex.Message);
                //}

                //    // Assert.That(""."")

                //}
                //Log.CloseAndFlush();

            }

        }
    }
}
