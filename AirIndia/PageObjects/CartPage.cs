using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class CartPage
    {
        IWebDriver driver;
        public CartPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'next-step-button')]")]
        private IWebElement? CheckOutButton { get; set; }

        public PaymentPage ClickCheckOutButton()
        {
            CheckOutButton?.Click();
            return new PaymentPage(driver);
        }
    }
}
