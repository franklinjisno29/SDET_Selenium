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
    internal class TravelerPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;

        public TravelerPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
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
            LastNameText?.Click();
            LastNameText?.SendKeys(lastName);
            EmailText?.Click();
            EmailText?.SendKeys(email);
            ConfirmEmailText?.Click();
            ConfirmEmailText?.SendKeys(confirmEmail);
            CountryCodesText?.Click();
            CountryCodesText?.SendKeys(countryCode);
            IWebElement countrycodeField = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'cdk-overlay-pane')]")));
            countrycodeField.Click();
            MobileNoText?.Click();
            MobileNoText?.SendKeys(mobileNo);
            CheckboxText?.Click();
            ConfirmButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(@class,'next-step-button')]")));
            return new CartPage(driver);
        }
    }
}
