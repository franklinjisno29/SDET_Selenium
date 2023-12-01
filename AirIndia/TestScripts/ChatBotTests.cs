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
    internal class ChatBotTests : CoreCodes
    {
        [Test, Category("Regression Test")]
        public void ChatBotTest()
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Chat Bot Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='floating-chat-bot-outer-wrapper']")));

            try
            {
                    fluentWait.Until(d => bchp);
                    bchp.ClickChatBot();
                    IWebElement num = driver.FindElement(By.XPath("//div[@class='flight-number']"));
                    string? numtext = num.Text;
                    TakeScreenshot();
                    Assert.That(numtext,Is.EqualTo("AI 692"));
                    LogTestResult("Chat Bot Test", "Chat Bot Test Success");
                    test = extent.CreateTest("Chat Bot Test - Passed");
                    test.Pass("Chat Bot Test Success");
                    bchp.closebot();
                }
                catch (AssertionException ex)
                {
                    TakeScreenshot();
                    LogTestResult("Chat Bot Test", "Chat Bot Test Failed", ex.Message);
                    test.Fail("Chat Bot Test Failed");
                }
        }
        
    }
}
