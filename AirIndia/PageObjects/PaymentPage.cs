using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class PaymentPage
    {
        IWebDriver driver;
        public PaymentPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.XPath, Using = "(//button[contains(@class,'mat-stroked-button')])[2]")]
        private IWebElement? InsuranceSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[contains(@class,'mat-expansion-indicator')])[2]")]
        private IWebElement? PaymentDropDown { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[contains(@class,'mat-expansion-indicator')])[2]")]
        private IWebElement? UPISelect { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[contains(@class,'mat-checkbox-inner-container')])[2]")]
        private IWebElement? ConditionsSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[contains(@class,'mat-flat-button')])[4]")]
        private IWebElement? PaySelect { get; set; }

        public void CheckOutProcess()
        {
            InsuranceSelect?.Click();
            Thread.Sleep(2000);
            PaymentDropDown?.Click();
            Thread.Sleep(2000);
            UPISelect?.Click();
            Thread.Sleep(2000);
            ConditionsSelect?.Click();
            Thread.Sleep(2000);
            PaySelect?.Click();
        }
    }
}
