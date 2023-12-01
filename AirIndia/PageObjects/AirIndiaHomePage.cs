using AirIndia.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.DOM;
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
    internal class AirIndiaHomePage
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;

        public AirIndiaHomePage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
            wait = new DefaultWait<IWebDriver>(driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//mat-radio-button[@id=\"mat-radio-2\"]")]
        private IWebElement? TravelOptions { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='From']")]
        private IWebElement? FromText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@id='To']")]
        private IWebElement? ToText { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@type='button']")]
        private IWebElement? DepartDateSelect { get; set; }

        [FindsBy(How = How.Id, Using = "dropdownForm1")]
        private IWebElement? PassengerCount { get; set; }

        [FindsBy(How = How.Id, Using = "mat-select-value-1")]
        private IWebElement? ClassSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//mat-select[@id='concession-type']")]
        private IWebElement? ConcessionTypeSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[text()=' SHOW FLIGHTS ']")]
        private IWebElement? ShowFlightsButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='signIn']")]
        private IWebElement? SignInButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//div[@class='mat-tab-label-content'])[4]")]
        private IWebElement? FlightStatusButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@name='flight-number-ip'])")]
        private IWebElement? FlightNumberText { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='mat-select-value-3']")]
        private IWebElement? DateText { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='floating-chat-bot-outer-wrapper']")]
        private IWebElement? ChatBotButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@id='header0menu0link3']")]
        private IWebElement? OffersButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//img[@title='Air India Logo']")]
        private IWebElement? LogoButton { get; set; }



        //Act
        public SearchResultPage SearchFlight(string from, string to, string dayselect, string monthselect, string yearselect, string passengers, string classselect, string concessiontype)
        {
            TravelOptions?.Click();
            FromText?.SendKeys(from);
            Thread.Sleep(1000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            FromText?.SendKeys(Keys.Enter);
            ToText?.SendKeys(to);
            Thread.Sleep(1000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            ToText?.SendKeys(Keys.Enter);
            DepartDateSelect?.Click();
            IWebElement doyField = wait.Until(d => d.FindElement(By.XPath("//select[@title='Select year']")));
            SelectElement selectyear = new SelectElement(doyField);
            selectyear.SelectByValue(yearselect);
            IWebElement domField = wait.Until(d => d.FindElement(By.XPath("//select[@title='Select month']")));
            SelectElement selectmonth = new SelectElement(domField);
            selectmonth.SelectByValue(monthselect);
            IWebElement dayField = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(),'" + dayselect + "')]")));
            dayField.Click();
            PassengerCount?.Click();
            IWebElement? passengercount = wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'plus-minus-holder')]//following::button")));
            int passenger = Convert.ToInt32(passengers);
            for (int i = 1; i < passenger; i++)
                passengercount.Click();
            ClassSelect?.Click();
            IWebElement classField = wait.Until(d => d.FindElement(By.XPath("//span[text()='" + classselect + "' and @class='mat-option-text']")));
            classField.Click();
            wait.Until(d => ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id='concession-type-panel']")));
            ConcessionTypeSelect?.Click();
            IWebElement concessionField = wait.Until(d => d.FindElement(By.XPath("//span[contains(text(),'" + concessiontype + "') and @class='mat-option-text']")));
            concessionField.Click();
            ShowFlightsButton?.Click();
            Thread.Sleep(30000);
            return new SearchResultPage(driver);
        }

        public SignInPage ClickSignIn()
        {
            SignInButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("createAccountButton")));
            return new SignInPage(driver);
        }

        public void ClickFlightStatus()
        {
            FlightStatusButton?.Click();
            FlightNumberText?.SendKeys("692");
            DateText?.Click();
            IWebElement dateField = wait.Until(d => d.FindElement(By.XPath("(//span[@class='mat-option-text'])[5]")));
            dateField.Click();
            ShowFlightsButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//p[contains(@class,'ontime-check-state')]")));
        }

        public void ClickFlightStatusInvalid()
        {
            FlightStatusButton?.Click();
            FlightNumberText?.SendKeys("123");
            DateText?.Click();
            IWebElement dateField = wait.Until(d => d.FindElement(By.XPath("(//span[@class='mat-option-text'])[5]")));
            dateField.Click();
            ShowFlightsButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h3[@class='error-message-part-subHeading']")));
        }

        public void ClickChatBot()
        {
            ChatBotButton?.Click();
            IWebElement FSbutton = wait.Until(d => d.FindElement(By.XPath("(//button[@class='response-button'])[4]")));
            FSbutton?.Click();
            IWebElement FNbutton = wait.Until(d => d.FindElement(By.XPath("(//button[@class='response-button'])[1]")));
            FNbutton?.Click();
            Thread.Sleep(4000);
            IWebElement FNtype = wait.Until(d => d.FindElement(By.Id("inputChat")));
            FNtype?.SendKeys("AI692");
            FNtype?.SendKeys(Keys.Enter);
            IWebElement doyField = wait.Until(d => d.FindElement(By.XPath("//select[@title='Select year']")));
            SelectElement selectyear = new SelectElement(doyField);
            selectyear.SelectByValue("2023");
            IWebElement domField = wait.Until(d => d.FindElement(By.XPath("//select[@title='Select month']")));
            SelectElement selectmonth = new SelectElement(domField);
            selectmonth.SelectByValue("12");
            IWebElement dayField = wait.Until(d => d.FindElement(By.XPath("//div[@class='ngb-dp-day' and contains(@aria-label,'10')]")));
            dayField.Click();
            Thread.Sleep(3000);
        }

        public void closebot()
        {
            IWebElement closebutton = wait.Until(d => d.FindElement(By.XPath("//img[@class='closs-icon']")));
            closebutton?.Click();
            IWebElement cancelbutton = wait.Until(d => d.FindElement(By.XPath("//button[@class='btn']")));
            cancelbutton?.Click();
        }

        public void ClickOffers()
        {
            OffersButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@id='first-tab']")));
        }
        
        public void ClickLogo()
        {
            LogoButton?.Click();
            IWebElement pageLoadedElement = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@title='Air India Logo']")));
        }
    }
}
