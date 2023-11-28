using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirIndia.PageObjects
{
    internal class TravelerPage
    {
        IWebDriver driver;
        public TravelerPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }

        //Arrange

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='firstName']")]
        private IWebElement? FirstNameText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@formcontrolname='lastName']")]
        private IWebElement? LastNameText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        private IWebElement? EmailText { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@type='email'])[2]")]
        private IWebElement? ConfirmEmailText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@aria-describedby='countryCodes']")]
        private IWebElement? CountryCodesText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='tel']")]
        private IWebElement? MobileNoText { get; set; }

        [FindsBy(How = How.ClassName, Using = "mat-checkbox-inner-container")]
        private IWebElement? CheckboxText { get; set; }

        [FindsBy(How = How.XPath, Using = "(//button[contains(@class,'mat-focus-indicator')])[4]")]
        private IWebElement? ConfirmButton { get; set; }

        //Act
        public CartPage FillPassengerDetails(string firstName, string lastName, string email, string confirmEmail, string countryCode, string mobileNo)
        {
            FirstNameText?.Click();
            FirstNameText?.SendKeys(firstName);
            Thread.Sleep(1000);
            LastNameText?.Click();
            LastNameText?.SendKeys(lastName);
            Thread.Sleep(1000);
            EmailText?.Click();
            EmailText?.SendKeys(email);
            Thread.Sleep(1000);
            ConfirmEmailText?.Click();
            ConfirmEmailText?.SendKeys(confirmEmail);
            Thread.Sleep(1000);
            CountryCodesText?.Click();
            CountryCodesText?.SendKeys(countryCode);
            Thread.Sleep(1000);
            IWebElement countrycodeField = driver.FindElement(By.XPath("//div[contains(@class,'cdk-overlay-pane')]"));
            countrycodeField.Click();
            Thread.Sleep(1000);
            MobileNoText?.Click();
            MobileNoText?.SendKeys(mobileNo);
            Thread.Sleep(1000);
            CheckboxText?.Click();
            Thread.Sleep(1000);
            ConfirmButton?.Click();
            return new CartPage(driver);
        }
    }
}
