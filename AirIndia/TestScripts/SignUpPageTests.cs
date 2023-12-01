using AirIndia.PageObjects;
using AirIndia.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.TestScripts
{
    internal class SignUpPageTests : CoreCodes
    {
        [Test, Category("Smoke Test")]
        public void SignUpValidTest()
        {
            var fluentWait = Waits(driver);
            driver.Navigate().GoToUrl("https://aiflyingreturns.b2clogin.com/aiflyingreturns.onmicrosoft.com/b2c_1a_signup_signin/oauth2/v2.0/authorize?client_id=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc&scope=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc%20openid%20profile%20offline_access&redirect_uri=https%3A%2F%2Fwww.airindia.com%2Fin%2Fen%2Fredirect.html&client-request-id=523613ea-8a16-4905-8966-1a7eb920b0aa&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=2.31.0&client_info=1&code_challenge=T5RJgNrfl4waYAyTXjWW4UPzwvTVa5SUwfQikvNrpqA&code_challenge_method=S256&nonce=e39f79fc-d574-4ffa-bbe9-fb7a0570ad5c&state=eyJpZCI6IjYzNjExZTA3LWFhMDktNGEwMy1iNWI2LWU0ZjhlZjAwOTdiMCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D%7C%2F");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
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
                    SignInPage signinpage = new SignInPage(driver);
                    var userDetailsPage = fluentWait.Until(d => signinpage.ClickJoinNow());
                    Log.Information("Join Now Clicked");
                    fluentWait.Until(d => userDetailsPage);
                    userDetailsPage.FillSignUp(firstName, lastName, dobday, dobmonth, dobyear);
                    Log.Information("Sign Up Test Started");
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

        [Test, Category("Smoke Test")]
        public void SignUpInValidTest()
        {
            var fluentWait = Waits(driver);
            driver.Navigate().GoToUrl("https://aiflyingreturns.b2clogin.com/aiflyingreturns.onmicrosoft.com/b2c_1a_signup_signin/oauth2/v2.0/authorize?client_id=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc&scope=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc%20openid%20profile%20offline_access&redirect_uri=https%3A%2F%2Fwww.airindia.com%2Fin%2Fen%2Fredirect.html&client-request-id=523613ea-8a16-4905-8966-1a7eb920b0aa&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=2.31.0&client_info=1&code_challenge=T5RJgNrfl4waYAyTXjWW4UPzwvTVa5SUwfQikvNrpqA&code_challenge_method=S256&nonce=e39f79fc-d574-4ffa-bbe9-fb7a0570ad5c&state=eyJpZCI6IjYzNjExZTA3LWFhMDktNGEwMy1iNWI2LWU0ZjhlZjAwOTdiMCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D%7C%2F");
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
            try
            {
                    SignInPage signinpage = new SignInPage(driver);
                    var userDetailsPage = fluentWait.Until(d => signinpage.ClickJoinNow());
                    Log.Information("Join Now Clicked");
                    fluentWait.Until(d => userDetailsPage);
                    userDetailsPage.ClickContinue();
                    IWebElement error = driver.FindElement(By.XPath("(//div[contains(@class,'itemLevel')])[2]"));
                    string? numtext = error.Text;
                    TakeScreenshot();
                    Assert.That(numtext, Does.Contain("Please select a title"));
                    LogTestResult("SignUp Invalid User", "SignUp Invalid User Success");
                    test = extent.CreateTest("SignUp Invalid User - Passed");
                    test.Pass("SignUp Invalid User Success");
                }
                catch (AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("SignUp Invalid User", "SignUp Invalid User Failed", ex.Message);
                    test.Fail("SignUp Invalid User Failed");
                }
        }

    }
}
