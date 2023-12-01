using AirIndia.Utilities;
using AirIndia.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.DevTools.V117.Debugger;
using Serilog;
using System.Threading.Tasks.Dataflow;
using SeleniumExtras.WaitHelpers;

namespace AirIndia.TestScripts
{
    [TestFixture]
    internal class SignUpTests : CoreCodes
    {
        [Test, Category("Regression Test")]
        public void SignUpTest()
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Sign Up Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";
            string? sheetName = "SearchFlight";
            List<SearchFlightData> searchFlightDataList = ExcelUtils.ReadSearchFlightData(excelFilePath, sheetName);
            foreach (var searchFlightData in searchFlightDataList)
            {
                try
                {
                    string? firstName = searchFlightData?.FirstName;
                    string? lastName = searchFlightData?.LastName;
                    string? dobday = searchFlightData?.DOBday;
                    string? dobmonth = searchFlightData?.DOBmonth;
                    string? dobyear = searchFlightData?.DOByear;

                    var signInPage = fluentWait.Until(d => bchp.ClickSignIn());
                    Log.Information("SignIn Clicked");
                    var userDetailsPage = fluentWait.Until(d => signInPage.ClickJoinNow());
                    Log.Information("Join Now Clicked");
                    fluentWait.Until(d => userDetailsPage);
                    userDetailsPage.FillSignUp(firstName, lastName, dobday, dobmonth, dobyear);
                    IWebElement button = driver.FindElement(By.Id("email_label"));
                    string? numtext = button.Text;
                    TakeScreenshot();
                    Assert.That(numtext, Does.Contain("EMAIL ADDRESS"));
                    LogTestResult("SignUp User", "SignUp User Success");
                    test = extent.CreateTest("SignUp User - Passed");
                    test.Pass("SignUp User Success");
                }
                catch (AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("SignUp User", "SignUp User Failed", ex.Message);
                    test.Fail("SignUp User Failed");
                }
            }

        }
    }

}
