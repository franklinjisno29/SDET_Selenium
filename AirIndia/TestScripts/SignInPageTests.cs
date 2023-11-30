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
    internal class SignInPageTests : CoreCodes
    {
        [Test, Order(1), Category("Smoke Test")]
        public void SignInInvalidEmailTest()
        {
            var fluentWait = Waits(driver);
            driver.Navigate().GoToUrl("https://aiflyingreturns.b2clogin.com/aiflyingreturns.onmicrosoft.com/b2c_1a_signup_signin/oauth2/v2.0/authorize?client_id=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc&scope=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc%20openid%20profile%20offline_access&redirect_uri=https%3A%2F%2Fwww.airindia.com%2Fin%2Fen%2Fredirect.html&client-request-id=523613ea-8a16-4905-8966-1a7eb920b0aa&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=2.31.0&client_info=1&code_challenge=T5RJgNrfl4waYAyTXjWW4UPzwvTVa5SUwfQikvNrpqA&code_challenge_method=S256&nonce=e39f79fc-d574-4ffa-bbe9-fb7a0570ad5c&state=eyJpZCI6IjYzNjExZTA3LWFhMDktNGEwMy1iNWI2LWU0ZjhlZjAwOTdiMCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D%7C%2F");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='next']")));
            SignInPage sip = new SignInPage(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Sign In Invalid Test Started");

            try
            {
                sip.ClickSignIn();
                Log.Information("Sign In Invalid Tested");
                TakeScreenshot();
                IWebElement button = driver.FindElement(By.XPath("//p[contains(text(),'Please fill')]"));
                string? numtext = button.Text;
                TakeScreenshot();
                Assert.That(numtext, Does.Contain("Please fill in the email"));
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Error");
                test = extent.CreateTest("Sign In Invalid Test - Passed");
                test.Pass("Sign In Invalid Test Error");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Failed", ex.Message);
                test.Fail("Sign In Invalid Test Failed");
            }
        }

        [Test, Order(2), Category("Smoke Test")]
        public void SignInInvalidPasswordTest()
        {
            var fluentWait = Waits(driver);
            driver.Navigate().GoToUrl("https://aiflyingreturns.b2clogin.com/aiflyingreturns.onmicrosoft.com/b2c_1a_signup_signin/oauth2/v2.0/authorize?client_id=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc&scope=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc%20openid%20profile%20offline_access&redirect_uri=https%3A%2F%2Fwww.airindia.com%2Fin%2Fen%2Fredirect.html&client-request-id=523613ea-8a16-4905-8966-1a7eb920b0aa&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=2.31.0&client_info=1&code_challenge=T5RJgNrfl4waYAyTXjWW4UPzwvTVa5SUwfQikvNrpqA&code_challenge_method=S256&nonce=e39f79fc-d574-4ffa-bbe9-fb7a0570ad5c&state=eyJpZCI6IjYzNjExZTA3LWFhMDktNGEwMy1iNWI2LWU0ZjhlZjAwOTdiMCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D%7C%2F");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='next']")));
            SignInPage sip = new SignInPage(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Sign In Invalid Test Started");

            try
            {
                sip.ClickSignInEmail();
                Log.Information("Sign In Invalid Tested");
                TakeScreenshot();
                IWebElement button = driver.FindElement(By.XPath("//p[contains(text(),'Please fill')]"));
                string? numtext = button.Text;
                TakeScreenshot();
                Assert.That(numtext, Does.Contain("Please fill in the password"));
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Error");
                test = extent.CreateTest("Sign In Invalid Test - Passed");
                test.Pass("Sign In Invalid Test Error");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Failed", ex.Message);
                test.Fail("Sign In Invalid Test Failed");
            }
        }

        [Test, Order(3), Category("Smoke Test")]
        public void SignInInvalidEmailPasswordTest()
        {
            var fluentWait = Waits(driver);
            driver.Navigate().GoToUrl("https://aiflyingreturns.b2clogin.com/aiflyingreturns.onmicrosoft.com/b2c_1a_signup_signin/oauth2/v2.0/authorize?client_id=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc&scope=ac5c8be3-c829-4db6-8eb7-aa4a37c61cbc%20openid%20profile%20offline_access&redirect_uri=https%3A%2F%2Fwww.airindia.com%2Fin%2Fen%2Fredirect.html&client-request-id=523613ea-8a16-4905-8966-1a7eb920b0aa&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=2.31.0&client_info=1&code_challenge=T5RJgNrfl4waYAyTXjWW4UPzwvTVa5SUwfQikvNrpqA&code_challenge_method=S256&nonce=e39f79fc-d574-4ffa-bbe9-fb7a0570ad5c&state=eyJpZCI6IjYzNjExZTA3LWFhMDktNGEwMy1iNWI2LWU0ZjhlZjAwOTdiMCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D%7C%2F");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='next']")));
            SignInPage sip = new SignInPage(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            Log.Information("Sign In Invalid Test Started");

            try
            {
                sip.ClickSignInEmailPassword();
                Log.Information("Sign In Invalid Tested");
                TakeScreenshot();
                IWebElement button = driver.FindElement(By.XPath("//p[contains(text(),'recognize')]"));
                string? numtext = button.Text;
                TakeScreenshot();
                Assert.That(numtext, Does.Contain("don't recognize"));
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Error");
                test = extent.CreateTest("Sign In Invalid Test - Passed");
                test.Pass("Sign In Invalid Test Error");
            }
            catch (AssertionException ex)
            {
                TakeScreenshot();
                LogTestResult("Sign In Invalid Test", "Sign In Invalid Test Failed", ex.Message);
                test.Fail("Sign In Invalid Test Failed");
            }
        }
    }
}
