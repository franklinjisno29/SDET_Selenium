using AirIndia.Utilities;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
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
            SortSelect?.Click();
            IWebElement sortField = driver.FindElement(By.XPath("(//button[contains(@role,'menuitem')])[2]"));
            sortField.Click();
            Thread.Sleep(2000);
            GetProductSelect(pId)?.Click();
            Thread.Sleep(2000);
            FareSelect?.Click();
            Thread.Sleep(2000);
            IWebElement comfortField = driver.FindElement(By.XPath("(//button[contains(@class,'mat-stroked-button')])[5]"));
            comfortField.Click();
            Thread.Sleep(2000);
            return new FlightPage(driver);
        }
    }
}
