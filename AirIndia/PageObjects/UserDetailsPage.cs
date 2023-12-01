using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.Page;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AirIndia.PageObjects
{
    internal class UserDetailsPage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;
        public UserDetailsPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout= TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));  
        }

        [FindsBy(How = How.Id, Using = "title")]
        private IWebElement? TitleText { get; set; }

        [FindsBy(How = How.Id, Using = "givenName")]
        private IWebElement? FirstNameText { get; set; }

        [FindsBy(How = How.Id, Using = "surname")]
        private IWebElement? LastNameText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[contains(@class,'datepicker-input')]")]
        private IWebElement? DOBText { get; set; }

        [FindsBy(How = How.Id, Using = "continueBtn")]
        private IWebElement? ContinueButton { get; set; }

        public void FillSignUp(string? firstName, string? lastName, string? dobday, string? dobmonth, string? dobyear)
        {
            TitleText?.Click();
            SelectElement title = new SelectElement(TitleText);
            title.SelectByValue("MR");
            IWebElement Element = wait.Until(ExpectedConditions.ElementToBeClickable(ContinueButton));
            driver.ExecuteJavaScript("arguments[0].scrollIntoView();", Element);
            FirstNameText?.Click();
            FirstNameText?.SendKeys(firstName);
            LastNameText?.Click();
            LastNameText?.SendKeys(lastName);
            wait.Until(d => ExpectedConditions.ElementToBeClickable(DOBText));
            DOBText?.Click();
            IWebElement cField = wait.Until(d => d.FindElement(By.XPath("//button[contains(@class,'prev-button')]")));
            for (int i=0;i<3;i++)
                cField?.Click();
            IWebElement dodField = wait.Until(d => d.FindElement(By.XPath("//span[text()='"+dobyear+"']")));
            dodField.Click();
            IWebElement domField = wait.Until(d => d.FindElement(By.XPath("//span[text()='"+dobmonth+"']")));
            domField?.Click();
            IWebElement dayField = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(),'" + dobday + "')]")));
            dayField.Click();
            
            ContinueButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email_label")));

        }

        public void ClickContinue()
        {
            wait.Until(d => ExpectedConditions.ElementToBeClickable(ContinueButton));
            ContinueButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[contains(@class,'itemLevel')])[2]")));

        }
    }
}
