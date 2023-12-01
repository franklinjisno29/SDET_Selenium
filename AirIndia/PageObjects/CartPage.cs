using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
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
        DefaultWait<IWebDriver> wait;

        public CartPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }
        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'next-step-button')]")]
        private IWebElement? CheckOutButton { get; set; }

        public void ClickCheckOutButton()
        {
            CheckOutButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class,'payment-checkout')]")));
        }
    }
}
