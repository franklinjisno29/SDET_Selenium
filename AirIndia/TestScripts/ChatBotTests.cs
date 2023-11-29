using AirIndia.PageObjects;
using AirIndia.Utilities;
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
            Thread.Sleep(2000);

                try
                {
                    fluentWait.Until(d => bchp);
                    bchp.ClickChatBot();
                    LogTestResult("Chat Bot Test", "Chat Bot Test Success");
                    test = extent.CreateTest("Chat Bot Test - Passed");
                    test.Pass("Chat Bot Test Success");
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
