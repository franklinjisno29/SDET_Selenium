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
        [FindsBy(How = How.LinkText, Using = "Water Poppy")]
        private IWebElement? ProductSelect { get; set; }

        //Act
        public string? GetProductSelect()
        {
            return ProductSelect?.Text;
        }
        public IWebElement GetProductSelect(string pId)
        {
            return driver.FindElement(By.XPath("(//div[@data-container='product-grid'])["+pId+"]"));
        }
            public ProductPage ClickProduct(string pId)
        {
            GetProductSelect(pId)?.Click();
            return new ProductPage(driver);
        }
    }
}
