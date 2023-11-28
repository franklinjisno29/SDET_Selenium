using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.DOM;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class AirIndiaHomePage : CoreCodes
    {
        IWebDriver driver;
        public AirIndiaHomePage(IWebDriver? driver)                               
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//mat-radio-button[@id=\"mat-radio-2\"]")]
        private IWebElement? TravelOptions { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='From']")]
        private IWebElement? FromText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='To']")]
        private IWebElement? ToText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='button']")]
        private IWebElement? DepartDateSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='button']")]
        private IWebElement? DaySelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@title='Select month']")]
        private IWebElement? MonthSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@title='Select year']")]
        private IWebElement? YearSelect { get; set; }

        [FindsBy(How = How.Id, Using = "dropdownForm1")]
        private IWebElement? PassengerCount { get; set; }

        [FindsBy(How = How.Id, Using = "mat-select-value-1")]
        private IWebElement? ClassSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-select[@id='concession-type']")]
        private IWebElement? ConcessionTypeSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()=' SHOW FLIGHTS ']")]
        private IWebElement? ShowFlightsButton { get; set; }

        //Act
        public SearchResultPage SearchFlight(string from, string to, string dayselect, string monthselect, string yearselect, string passengers, string classselect, string concessiontype)
        {
            //IWebElement modal = new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("(//div[@class='modal-inner-wrap'])[position()=2]")));
            TravelOptions?.Click();
            FromText?.SendKeys(from);
            Thread.Sleep(1000);
            FromText?.SendKeys(Keys.Enter);
            ToText?.SendKeys(to);
            Thread.Sleep(1000);
            ToText?.SendKeys(Keys.Enter);
            DepartDateSelect?.Click();
            IWebElement doyField = driver.FindElement(By.XPath("//select[@title='Select year']"));
            SelectElement selectyear = new SelectElement(doyField);
            selectyear.SelectByValue(yearselect);
            Thread.Sleep(1000);
            IWebElement domField = driver.FindElement(By.XPath("//select[@title='Select month']"));
            SelectElement selectmonth = new SelectElement(domField);
            selectmonth.SelectByValue(monthselect);
            Thread.Sleep(1000);
            IWebElement dayField = driver.FindElement(By.XPath("//span[contains(text(),'"+dayselect+"')]"));
            dayField.Click();
            Thread.Sleep(1000);
            PassengerCount?.Click();
            Thread.Sleep(1000);
            IWebElement? passengercount = driver.FindElement(By.XPath("//div[contains(@class,'plus-minus-holder')]//following::button"));
            int passenger = Convert.ToInt32(passengers);
            for(int i=1;i<passenger;i++)
                passengercount.Click();
            Thread.Sleep(1000);
            ClassSelect?.Click();
            IWebElement classField = driver.FindElement(By.XPath("//span[text()='"+classselect+"' and @class='mat-option-text']"));
            classField.Click();
            Thread.Sleep(1000);
            ConcessionTypeSelect?.Click();
            IWebElement concessionField = driver.FindElement(By.XPath("//span[contains(text(),'"+concessiontype+"') and @class='mat-option-text']"));
            concessionField.Click();
            Thread.Sleep(1000);
            ShowFlightsButton?.Click();
            Thread.Sleep(30000);
            return new SearchResultPage(driver);
        }
    }
}
