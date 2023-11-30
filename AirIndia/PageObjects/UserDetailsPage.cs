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
    internal class UserDetailsPage: CoreCodes
    {
        IWebDriver driver;
        public UserDetailsPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
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
            var fluentWait = Waits(driver);
            TitleText?.Click();
            SelectElement title = new SelectElement(TitleText);
            title.SelectByValue("MR");
            IWebElement element = driver.FindElement(By.Id("continueBtn"));
            driver.ExecuteJavaScript("arguments[0].scrollIntoView();", element);
            FirstNameText?.Click();
            FirstNameText?.SendKeys(firstName);
            LastNameText?.Click();
            LastNameText?.SendKeys(lastName);
            Thread.Sleep(3000);
            DOBText?.Click();
            Thread.Sleep(3000);
            IWebElement cField = fluentWait.Until(d => d.FindElement(By.XPath("//button[contains(@class,'prev-button')]")));
            for (int i=0;i<3;i++)
                cField?.Click();
            IWebElement dodField = fluentWait.Until(d => d.FindElement(By.XPath("//span[text()='"+dobyear+"']")));
            dodField.Click();
            IWebElement domField = fluentWait.Until(d => d.FindElement(By.XPath("//span[text()='"+dobmonth+"']")));
            domField?.Click();
            IWebElement dayField = fluentWait.Until(d => d.FindElement(By.XPath("//span[contains(text(),'" + dobday + "')]")));
            dayField.Click();
            
            ContinueButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("email_label")));

        }

        public void ClickContinue()
        {
            var fluentWait = Waits(driver);
            Thread.Sleep(2000);
            ContinueButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("(//div[contains(@class,'itemLevel')])[2]")));

        }
    }
}
