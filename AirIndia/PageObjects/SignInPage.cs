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
    internal class SignInPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;

        public SignInPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
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
            JoinNowButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("title")));
            return new UserDetailsPage(driver);
        }
        public void ClickSignIn()
        {
            SignInButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Please fill')]")));
        }
        public void ClickSignInEmail()
        {
            EmailText?.SendKeys("frank@gmail.com");
            SignInButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'Please fill')]")));
        }

        public void ClickSignInEmailPassword()
        {
            EmailText?.SendKeys("frank@gmail.com");
            PasswordText?.SendKeys("1234");
            SignInButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(text(),'recognize')]")));
        }
    }
}
