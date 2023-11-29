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
    internal class HomePageTests : CoreCodes
    {
        [Test, Category("Smoke Test")]
        public void FlightStatusTest()
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            AirIndiaHomePage bchp = new AirIndiaHomePage(driver);
            Log.Information("Flight Status Test Started");
            Thread.Sleep(2000);
                try
                {
                    fluentWait.Until(d => bchp);
                    bchp.ClickFlightStatus();
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
    }
}

