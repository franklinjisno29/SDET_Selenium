using AirIndia.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class FlightPage : CoreCodes
    {
        IWebDriver driver;
        public FlightPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }

        //Arrange

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'passenger details')]")]
        private IWebElement? PassengerDetails { get; set; }

        //Act
        public TravelerPage ClickPassengerDetails()
        {
            var fluentWait = Waits(driver);
            PassengerDetails?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@formcontrolname='firstName']")));
            return new TravelerPage(driver);

        }
    }
}
