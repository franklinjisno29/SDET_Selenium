using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class SearchResultPage : CoreCodes
    {
        IWebDriver driver;
        public SearchResultPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//div[contains(@class,'menu-title')]")]
        private IWebElement? SortSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='selectFare'][1]")]
        private IWebElement? FareSelect { get; set; }

        //Act
        //public string? GetProductSelect()
        //{
        //    return ProductSelect?.Text;
        //}
        public IWebElement GetProductSelect(string pId)
        {
            return driver.FindElement(By.XPath("(//button[@type='button' and contains(@class,'flight-card-button')])["+pId+"]"));
        }
        public FlightPage ClickProduct(string pId)
        {
            var fluentWait = Waits(driver);
            SortSelect?.Click();
            IWebElement sortField = fluentWait.Until(d => d.FindElement(By.XPath("(//button[contains(@role,'menuitem')])[2]")));
            sortField.Click();
            GetProductSelect(pId)?.Click();
            FareSelect?.Click();
            IWebElement comfortField = fluentWait.Until(d => d.FindElement(By.XPath("(//button[contains(@class,'mat-stroked-button')])[5]")));
            comfortField.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[contains(text(),'passenger details')]")));
            return new FlightPage(driver);
            }
    }
}
