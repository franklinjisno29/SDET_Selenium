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
    internal class SignInPage: CoreCodes
    {
        IWebDriver driver;
        public SignInPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
        }

        [FindsBy(How = How.Id, Using = "createAccountButton")]
        private IWebElement? JoinNowButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='next']")]
        private IWebElement? SignInButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='signInName']")]
        private IWebElement? EmailText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='password']")]
        private IWebElement? PasswordText { get; set; }

        public UserDetailsPage ClickJoinNow()
        {
            var fluentWait = Waits(driver);
            JoinNowButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
            return new UserDetailsPage(driver);
        }
        public void ClickSignIn()
        {
            var fluentWait = Waits(driver);
            SignInButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Please fill')]")));
        }
        public void ClickSignInEmail()
        {
            var fluentWait = Waits(driver);
            EmailText?.SendKeys("frank@gmail.com");
            SignInButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Please fill')]")));
        }

        public void ClickSignInEmailPassword()
        {
            var fluentWait = Waits(driver);
            EmailText?.SendKeys("frank@gmail.com");
            PasswordText?.SendKeys("1234");
            SignInButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'recognize')]")));
        }
    }
}
